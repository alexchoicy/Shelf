import { queryOptions } from "@tanstack/react-query";
import type { components } from "@/data/APIschema";
import { $APIFetch } from "../APIFetchClient";

export const characterQueries = {
	getCharacterSearchList: () =>
		queryOptions({
			queryKey: ["characters", "searchList"],
			queryFn: async () => {
				const result = await $APIFetch<
					components["schemas"]["CharacterListModel"][]
				>("/characters/list", {
					method: "GET",
				});
				if (!result.ok) return [];
				return result.data;
			},
		}),
};
