import { queryOptions } from "@tanstack/react-query";
import type { components } from "@/data/APIschema";
import { $APIFetch } from "../APIFetchClient";

export const meQueries = {
	getMe: () =>
		queryOptions({
			queryKey: ["me"],
			queryFn: async () => {
				const response = await $APIFetch<components["schemas"]["UserDto"]>(
					"/me",
					{
						method: "GET",
					},
				);
				if (!response.ok) throw new Error("Failed to fetch user data");
				return response.data;
			},
			staleTime: 1000 * 60 * 5,
		}),
};
