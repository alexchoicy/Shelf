import { useQuery } from "@tanstack/react-query";
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
} from "@/components/shadcn/combobox";
import type { components } from "@/data/APIschema";
import { characterQueries } from "@/lib/queries/character.queries";

export default function CharacterCombobox() {
	const { data: character } = useQuery(
		characterQueries.getCharacterSearchList(),
	);
	const anchor = useComboboxAnchor();

	return (
		<Combobox items={character} multiple>
			<ComboboxChips ref={anchor}>
				<ComboboxValue>
					{(items: components["schemas"]["CharacterListModel"][]) => (
						<>
							{items.map(
								(item: components["schemas"]["CharacterListModel"]) => (
									<ComboboxChip key={item.characterId}>
										{item.characterName}
									</ComboboxChip>
								),
							)}
							<ComboboxChipsInput />
						</>
					)}
				</ComboboxValue>
			</ComboboxChips>
			<ComboboxContent anchor={anchor}>
				<ComboboxEmpty>No characters found.</ComboboxEmpty>
				<ComboboxList>
					{(item: components["schemas"]["CharacterListModel"]) => (
						<ComboboxItem key={item.characterId} value={item.characterId}>
							{item.characterName}
						</ComboboxItem>
					)}
				</ComboboxList>
			</ComboboxContent>
		</Combobox>
	);
}
