import type { components } from "@/data/APIschema";

type WorkCreditRole = components["schemas"]["WorkCreditRole"];

export const WORK_CREDIT_ROLE: Record<WorkCreditRole, string> = {
	ARTIST: "Artist",
	STUDIO: "Studio",
} as const;

export const WORK_CREDIT_ROLE_OPTIONS = Object.entries(WORK_CREDIT_ROLE).map(
	([value, label]) => ({
		value,
		label,
	}),
);
