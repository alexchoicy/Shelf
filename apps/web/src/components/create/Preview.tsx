import { ImageIcon } from "lucide-react";
import type { components } from "@/data/APIschema";
import { Badge } from "../shadcn/badge";

type FormRequest = components["schemas"]["WorkCreationRequest"];
type PartyList = components["schemas"]["PartyListDto"];
type Props = {
	FormInfo: FormRequest;
	uploadItems: Record<string, File>;
	partySearchList: PartyList[];
	cover?: Blob | null;
	setCover?: (cover: Blob | null) => void;
};

export default function Preview({ FormInfo, partySearchList, cover }: Props) {
	return (
		<div className="flex gap-5">
			<div className="relative w-80 aspect-video shrink-0 rounded-lg flex items-center justify-center bg-muted shadow-lg overflow-hidden">
				{cover ? (
					<img
						src={URL.createObjectURL(cover)}
						alt="Cover Preview"
						className="w-full h-full object-cover"
					/>
				) : (
					<ImageIcon className="h-5 w-5" />
				)}
			</div>
			<div className="flex flex-1 flex-col py-1">
				<Badge variant="outline" className="mb-2 w-fit">
					{FormInfo.type}
				</Badge>
				<h3 className="text-lg font-semibold text-foreground leading-tight">
					{FormInfo.title || "Untitled Work"}
				</h3>
				<p className="mt-1 text-sm text-muted-foreground">
					by{" "}
					{FormInfo.credits &&
						FormInfo.credits.length > 0 &&
						FormInfo.credits.map((credit, idx) => {
							const party = partySearchList.find(
								(p) => p.partyId === credit.partyId,
							);
							return (
								<span key={`${credit.partyId}-${idx}`}>
									{party?.partyName || "Unknown"}
									{credit.role ? ` (${String(credit.role).toLowerCase()})` : ""}
									{idx < FormInfo.credits.length - 1 ? ", " : ""}
								</span>
							);
						})}
				</p>
			</div>

			<div className="mt-auto flex items-center gap-4 pt-4 text-xs text-muted-foreground">
				<span className="flex items-center gap-1">
					{FormInfo.mediaItems.filter((m) => m.kind === "MAIN").length} main
				</span>
				<span className="flex items-center gap-1">
					{FormInfo.mediaItems.filter((m) => m.kind === "EXTRA").length} extra
				</span>
			</div>
		</div>
	);
}
