import { queryOptions } from "@tanstack/react-query";
import type { components } from "@/data/APIschema";
import { $APIFetch } from "@/lib/APIFetchClient";

export const authQueries = {
	me: () =>
		queryOptions({
			queryKey: ["auth", "me"],
			queryFn: async () => {
				const result = await $APIFetch("/auth", {
					method: "GET",
				});
				if (!result.ok) throw new Error("Failed to fetch user");
				return result.data;
			},
			staleTime: 1000 * 60 * 5,
		}),
};

export const authMutations = {
	login: {
		mutationFn: async (data: components["schemas"]["LoginRequestDto"]) => {
			const result = await $APIFetch<components["schemas"]["LoginResponseDto"]>(
				"/auth/login",
				{
					method: "POST",
					body: JSON.stringify(data),
				},
			);
			if (!result.ok) {
				throw new Error("Invalid username or password");
			}
			return result.data;
		},
	},

	logout: {
		mutationFn: async () => {
			const result = await $APIFetch("/auth/logout", {
				method: "POST",
			});
			if (!result.ok) throw new Error("Logout failed");
			return result.data;
		},
	},
};
