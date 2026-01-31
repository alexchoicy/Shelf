import { startTransition, useEffect, useMemo, useRef, useState } from "react";
import type { components } from "@/data/APIschema";
import {
	Combobox,
	ComboboxChip,
	ComboboxChips,
	ComboboxChipsInput,
	ComboboxContent,
	ComboboxEmpty,
	ComboboxItem,
	ComboboxList,
	ComboboxValue,
	useComboboxAnchor,
} from "../shadcn/combobox";

type PartyList = components["schemas"]["PartyListDto"];

type Props = {
	parties: PartyList[];
	selectedValues: PartyList[];
	setSelectedValues: (valies: PartyList[]) => void;
};

//TODO rebuild this later
export default function PartyCombobox({
	parties,
	selectedValues,
	setSelectedValues,
}: Props) {
	const [searchResults, setSearchResults] = useState<PartyList[]>(
		() => parties,
	);
	const [searchValue, setSearchValue] = useState("");
	const abortControllerRef = useRef<AbortController | null>(null);

	useEffect(() => {
		if (searchValue === "") {
			setSearchResults(parties);
		}
	}, [parties, searchValue]);

	const items = useMemo(() => {
		if (selectedValues.length === 0) {
			return searchResults;
		}
		const merged = [...searchResults];

		selectedValues.forEach((party) => {
			if (!searchResults.some((result) => result.partyId === party.partyId)) {
				merged.push(party);
			}
		});

		return merged;
	}, [searchResults, selectedValues]);

	const normalize = (str: string) => str.trim().toLocaleUpperCase("en-US");

	const searchParty = async (query: string): Promise<PartyList[]> => {
		const normalizedQuery = normalize(query);
		const filter = parties.filter(
			(party) =>
				party.partyNormalizedName.includes(normalizedQuery) ||
				party.partyAliases.some((alias) =>
					alias.aliasNormalizedName.includes(normalizedQuery),
				),
		);

		return filter;
	};

	const anchor = useComboboxAnchor();
	return (
		<Combobox
			items={items}
			filter={null}
			multiple
			itemToStringLabel={(party: PartyList) => party.partyName}
			onOpenChangeComplete={(open) => {
				if (!open) {
					setSearchResults(parties);
				}
			}}
			onValueChange={(nextSelectedValues: PartyList[]) => {
				setSelectedValues(nextSelectedValues);
				setSearchValue("");

				if (nextSelectedValues.length === 0) {
					setSearchResults(parties);
				}
			}}
			onInputValueChange={(nextSearchValue, { reason }) => {
				setSearchValue(nextSearchValue);

				const controller = new AbortController();
				abortControllerRef.current?.abort();
				abortControllerRef.current = controller;

				if (nextSearchValue === "") {
					setSearchResults(parties);
					return;
				}
				if (reason === "item-press") {
					return;
				}
				startTransition(async () => {
					const result = await searchParty(nextSearchValue);

					if (controller.signal.aborted) {
						return;
					}

					startTransition(() => {
						setSearchResults(result);
					});
				});
			}}
		>
			<ComboboxChips ref={anchor}>
				<ComboboxValue>
					{selectedValues.map((value: PartyList) => (
						<ComboboxChip key={value.partyId}>{value.partyName}</ComboboxChip>
					))}
					<ComboboxChipsInput />
				</ComboboxValue>
			</ComboboxChips>

			<ComboboxContent anchor={anchor}>
				<ComboboxEmpty>No parties found.</ComboboxEmpty>
				<ComboboxList>
					{(item: PartyList) => (
						<ComboboxItem key={item.partyId} value={item}>
							{item.partyName}
						</ComboboxItem>
					)}
				</ComboboxList>
			</ComboboxContent>
		</Combobox>
	);
}
