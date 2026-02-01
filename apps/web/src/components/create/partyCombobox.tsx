import { startTransition, useEffect, useMemo, useRef, useState } from "react";
import type { components } from "@/data/APIschema";
import { Button } from "../shadcn/button";
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
import {
	Dialog,
	DialogClose,
	DialogContent,
	DialogFooter,
	DialogHeader,
	DialogTitle,
} from "../shadcn/dialog";
import { Input } from "../shadcn/input";
import { Label } from "../shadcn/label";

type PartyList = components["schemas"]["PartyListDto"];

type Props = {
	parties: PartyList[];
	selectedValues: PartyList[];
	setSelectedValues: React.Dispatch<React.SetStateAction<PartyList[]>>;
};

type CreatablePartyItem = {
	type: "create";
	creatable: string; // raw user text
};

type PartyItem = PartyList | CreatablePartyItem;

function isCreatable(item: PartyItem): item is CreatablePartyItem {
	return (item as any).type === "create";
}

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
	const [openDialog, setOpenDialog] = useState(false);
	const anchor = useComboboxAnchor();
	const highlightedItemRef = useRef<PartyList | undefined>(undefined);
	const pendingQueryRef = useRef("");
	const createInputRef = useRef<HTMLInputElement | null>(null);
	const comboboxInputRef = useRef<HTMLInputElement | null>(null);

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

	const serachPartyFind = async (query: string): Promise<PartyList | null> => {
		const normalizedQuery = normalize(query);
		const found = parties.find(
			(party) =>
				party.partyNormalizedName === normalizedQuery ||
				party.partyAliases.some(
					(alias) => alias.aliasNormalizedName === normalizedQuery,
				),
		);
		return found || null;
	};

	async function handleInputKeyDown(
		event: React.KeyboardEvent<HTMLInputElement>,
	) {
		if (event.key !== "Enter" || highlightedItemRef.current) {
			return;
		}

		const currentTrimmed = searchValue.trim();
		if (currentTrimmed === "") {
			return;
		}

		const existing = await serachPartyFind(currentTrimmed);

		if (existing) {
			setSelectedValues((prev: PartyList[]) =>
				prev.some((item: PartyList) => item.partyId === existing.partyId)
					? prev
					: [...prev, existing],
			);

			setSearchValue("");
			return;
		}

		pendingQueryRef.current = currentTrimmed;
		setOpenDialog(true);
	}

	const trimmed = searchValue.trim();
	const normalizedTrimmed = normalize(trimmed);

	const exactExists = useMemo(() => {
		if (trimmed === "") return true;
		return parties.some(
			(p) =>
				p.partyNormalizedName === normalizedTrimmed ||
				p.partyAliases.some((a) => a.aliasNormalizedName === normalizedTrimmed),
		);
	}, [parties, trimmed, normalizedTrimmed]);

	const itemsForView = useMemo<PartyItem[]>(() => {
		if (trimmed !== "" && !exactExists) {
			return [...items, { type: "create", creatable: trimmed }];
		}
		return items;
	}, [items, trimmed, exactExists]);

	const handleCreateSave = () => {
		const input = createInputRef.current || comboboxInputRef.current;
		const value = input ? input.value.trim() : "";
	};

	return (
		<>
			<Combobox
				items={itemsForView}
				filter={null}
				multiple
				itemToStringLabel={(item: PartyItem) =>
					isCreatable(item) ? `Create "${item.creatable}"` : item.partyName
				}
				onOpenChangeComplete={(open) => {
					if (!open) {
						setSearchResults(parties);
					}
				}}
				onValueChange={(next: PartyItem[]) => {
					const createRow = next.find(isCreatable);

					if (createRow) {
						pendingQueryRef.current = createRow.creatable;
						setOpenDialog(true);
						return;
					}

					// only real parties
					const clean = next.filter((x) => !isCreatable(x)) as PartyList[];

					setSelectedValues(clean);
					setSearchValue("");

					if (clean.length === 0) {
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
						<ComboboxChipsInput
							onKeyDown={handleInputKeyDown}
							ref={comboboxInputRef}
						/>
					</ComboboxValue>
				</ComboboxChips>

				<ComboboxContent anchor={anchor}>
					<ComboboxEmpty>No parties found.</ComboboxEmpty>
					<ComboboxList>
						{(item: PartyItem) =>
							isCreatable(item) ? (
								<ComboboxItem
									key={`create:${normalize(item.creatable)}`}
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
			<Dialog open={openDialog} onOpenChange={setOpenDialog}>
				<DialogContent initialFocus={createInputRef}>
					<DialogHeader>
						<DialogTitle>Create Party</DialogTitle>
					</DialogHeader>

					<div className="flex items-center gap-2">
						<div className="grid flex-1 gap-2">
							<Label htmlFor="name" className="sr-only">
								Name
							</Label>
							<Input
								id="name"
								ref={createInputRef}
								defaultValue={pendingQueryRef.current}
							/>
						</div>
					</div>
					<DialogFooter>
						<DialogClose render={<Button variant="outline">Cancel</Button>} />
						<Button type="submit" onClick={handleCreateSave}>
							Save changes
						</Button>
					</DialogFooter>
				</DialogContent>
			</Dialog>
		</>
	);
}
