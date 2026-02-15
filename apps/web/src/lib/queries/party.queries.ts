import { queryOptions } from "@tanstack/react-query";
import type { components } from "@/data/APIschema";
import { $APIFetch } from "../APIFetchClient";

export const partyQueries = {
	getPartySearchList: () =>
		queryOptions({
			queryKey: ["parties", "searchList"],
			queryFn: async () => {
				const result = await $APIFetch<
					components["schemas"]["PartyListModel"][]
				>("/parties/list", {
					method: "GET",
				});
				if (!result.ok) return [];
				return result.data;
			},
		}),
};

export const partyMutations = {
	create: {
		mutationFn: async (data: components["schemas"]["CreatePartyRequest"]) => {
			const result = await $APIFetch("/parties", {
				method: "POST",
				body: JSON.stringify(data),
			});
			if (!result.ok) {
				throw new Error("Failed to create party");
			}
			return result.ok;
		},
	},
};
