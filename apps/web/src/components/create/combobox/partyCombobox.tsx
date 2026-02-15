import { Combobox as BaseCombobox } from "@base-ui/react/combobox";
import { useSuspenseQuery } from "@tanstack/react-query";
import { useMemo, useRef, useState } from "react";
import {
	Combobox,
	ComboboxChip,
	ComboboxChips,
	ComboboxChipsInput,
	ComboboxContent,
	ComboboxItem,
	ComboboxList,
	ComboboxValue,
	useComboboxAnchor,
} from "@/components/shadcn/combobox";
import type { components } from "@/data/APIschema";
import { partyQueries } from "@/lib/queries/party.queries";
import { normalizeString } from "@/lib/utils/string";

type PartyList = components["schemas"]["PartyListModel"];

type CreatablePartyItem = {
	type: "create";
	creatable: string;
};

type PartyItem = PartyList | CreatablePartyItem;

function isCreatable(item: PartyItem): item is CreatablePartyItem {
	return "type" in item && item.type === "create";
}

// bro i am smart, this less complex then the official example
export default function PartyCombobox() {
	const { data: parties } = useSuspenseQuery(partyQueries.getPartySearchList());
	const [searchResults, setSearchResults] = useState<PartyList[]>(parties);
	const [selected, setSelected] = useState<PartyList[]>([]);
	const [query, setQuery] = useState("");

	const comboboxInputRef = useRef<HTMLInputElement | null>(null);
	const selectedValuesRef = useRef<PartyList[]>([]);

	const anchor = useComboboxAnchor();

	const { contains } = BaseCombobox.useFilter();

	const items = useMemo((): PartyItem[] => {
		if (query === "") {
			return parties;
		}

		const merged: PartyItem[] = [...searchResults];

		if (searchResults.length === 0) {
			merged.push({ type: "create", creatable: query.trim() });
		}

		if (selected.length === 0) {
			return merged;
		}

		selected.forEach((party) => {
			if (!searchResults.some((result) => result.partyId === party.partyId)) {
				merged.push(party);
			}
		});

		return merged;
	}, [searchResults, selected, parties, query]);

	const searchParties = (
		query: string,
		filter: (item: string, query: string) => boolean,
	) => {
		const normalizedQuery = normalizeString(query);

		const filtered = parties.filter((party) => {
			return (
				filter(party.partyNormalizedName, normalizedQuery) ||
				party.partyAliases.some((alias) =>
					filter(alias.aliasNormalizedName, normalizedQuery),
				)
			);
		});

		return filtered;
	};

	return (
		<Combobox
			items={items}
			itemToStringLabel={(item: PartyItem) =>
				isCreatable(item) ? `Create "${item.creatable}"` : item.partyName
			}
			multiple
			filter={null}
			onValueChange={(next) => {
				const createRow = next.find(isCreatable);
				if (createRow) {
					console.log("Creating new party with name:", query);
					return;
				}

				const clean = next.filter((x): x is PartyList => !isCreatable(x));
				selectedValuesRef.current = clean;
				setSelected(clean);
				setQuery("");
				if (clean.length === 0) {
					setSearchResults(parties);
				}
			}}
			onInputValueChange={(next, { reason }) => {
				setQuery(next);

				if (next === "") {
					setSearchResults(selectedValuesRef.current);
					return;
				}

				if (reason === "item-press") {
					return;
				}

				const results = searchParties(next, contains);

				setSearchResults(results);
			}}
		>
			<ComboboxChips ref={anchor}>
				<ComboboxValue>
					{selected.map((value: PartyList) => (
						<ComboboxChip key={value.partyId}>{value.partyName}</ComboboxChip>
					))}
					<ComboboxChipsInput ref={comboboxInputRef} />
				</ComboboxValue>
			</ComboboxChips>
			<ComboboxContent anchor={anchor}>
				<ComboboxList>
					{(item: PartyItem) =>
						isCreatable(item) ? (
							<ComboboxItem
								key={`create:${normalizeString(item.creatable)}`}
								value={item}
							>
								Create &quot;{item.creatable}&quot;
							</ComboboxItem>
						) : (
							<ComboboxItem key={item.partyId} value={item}>
								{item.partyName}
							</ComboboxItem>
						)
					}
				</ComboboxList>
			</ComboboxContent>
		</Combobox>
	);
}
