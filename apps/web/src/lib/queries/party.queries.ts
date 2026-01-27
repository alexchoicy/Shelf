import { queryOptions } from "@tanstack/react-query";
import type { components } from "@/data/APIschema";
import { $APIFetch } from "../APIFetchClient";

export const partyQueries = {
	getPartySearchList: () =>
		queryOptions({
			queryKey: ["parties", "searchList"],
			queryFn: async () => {
				const result = await $APIFetch<components["schemas"]["PartyListDto"][]>(
					"/parties/list",
					{
						method: "GET",
					},
				);
				if (!result.ok) return [];
				return result.data;
			},
		}),
};
