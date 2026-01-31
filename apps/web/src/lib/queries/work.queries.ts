import type { components } from "@/data/APIschema";
import { $APIFetch } from "@/lib/APIFetchClient";

export const workMutations = {
	create: {
		mutationFn: async (data: components["schemas"]["WorkCreationRequest"]) => {
			const result = await $APIFetch<
				components["schemas"]["WorkCreationResponse"]
			>("/works", {
				method: "POST",
				body: JSON.stringify(data),
			});
			if (!result.ok) {
				throw new Error("Failed to create work");
			}
			return result.data;
		},
	},
};
