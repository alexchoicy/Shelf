import type { components } from "@/data/APIschema";

type PartyType = components["schemas"]["PartyType"];

export const PARTY_TYPE: Record<PartyType, string> = {
	INDIVIDUAL: "Individual",
	GROUP: "Group",
	ORGANIZATION: "Organization",
	STUDIO: "Studio",
} as const;

export const PARTY_TYPE_OPTIONS = Object.entries(PARTY_TYPE).map(
	([value, label]) => ({
		value,
		label,
	}),
);
