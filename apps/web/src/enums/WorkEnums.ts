import type { components } from "@/data/APIschema";

type WorkMedium = components["schemas"]["WorkMedium"];
type WorkRating = components["schemas"]["WorkRating"];
type WorkType = components["schemas"]["WorkType"];
type WorkVisibility = components["schemas"]["WorkVisibility"];

export const WORK_MEDIUM: Record<WorkMedium, string> = {
	IMAGE: "Image",
	VIDEO: "Video",
	AUDIO: "Audio",
	TEXT: "Text",
} as const;

export const WORK_TYPE: Record<WorkType, string> = {
	ILLUSTRATION: "Illustration",
	MANGA: "Manga",
	WEBTOON: "Webtoon",
	COMIC: "Comic",
	PHOTOSET: "Photoset",
	ANIME: "Anime",
	OVA: "OVA",
	PMV: "PMV",
	HMV: "HMV",
	CONCERT: "Concert",
	VIDEO: "Video",
	MUSIC: "Music",
	ASMR: "ASMR",
	NOVEL: "Novel",
	LIGHT_NOVEL: "Light Novel",
	TEXTONLY: "Text Only",
	PORN: "Pornography",
	JAV: "JAV",
} as const;

export const WORK_RATING: Record<WorkRating, string> = {
	GENERAL: "General",
	MATURE: "Mature",
	ADULT: "Adult",
} as const;

export const WORK_VISIBILITY: Record<WorkVisibility, string> = {
	PUBLIC: "Public",
	HIDDEN: "Hidden",
} as const;

export const WORK_MEDIUM_OPTIONS = Object.entries(WORK_MEDIUM).map(
	([value, label]) => ({
		value,
		label,
	}),
);

export const WORK_TYPE_OPTIONS = Object.entries(WORK_TYPE).map(
	([value, label]) => ({
		value,
		label,
	}),
);

export const WORK_RATING_OPTIONS = Object.entries(WORK_RATING).map(
	([value, label]) => ({
		value,
		label,
	}),
);

export const WORK_VISIBILITY_OPTIONS = Object.entries(WORK_VISIBILITY).map(
	([value, label]) => ({
		value,
		label,
	}),
);

// Medium -> Type mappings
export const WORK_TYPE_BY_MEDIUM: Record<WorkMedium, WorkType[]> = {
	IMAGE: ["ILLUSTRATION", "MANGA", "WEBTOON", "COMIC", "PHOTOSET"],
	VIDEO: ["ANIME", "OVA", "PMV", "HMV", "CONCERT", "PORN", "JAV", "VIDEO"],
	AUDIO: ["MUSIC", "ASMR"],
	TEXT: ["NOVEL", "LIGHT_NOVEL", "TEXTONLY"],
} as const;

export const getWorkTypesByMedium = (medium: WorkMedium) =>
	WORK_TYPE_OPTIONS.filter((option) =>
		WORK_TYPE_BY_MEDIUM[medium].includes(option.value as WorkType),
	);
