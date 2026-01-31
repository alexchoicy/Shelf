import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import {
	FileAudio,
	FileText,
	FileVideo,
	GripVertical,
	ImageIcon,
	Star,
} from "lucide-react";
import { memo, useMemo } from "react";
import type { components } from "@/data/APIschema";
import { Button } from "../shadcn/button";
import { Card } from "../shadcn/card";
import {
	Select,
	SelectContent,
	SelectItem,
	SelectTrigger,
	SelectValue,
} from "../shadcn/select";

type MediaItem = components["schemas"]["MediaItemCreationRequest"];
type MediaItemKind = components["schemas"]["MediaItemKind"];

type Props = {
	currentCoverSourceHash: string;
	onToggleCover: (fileHash: string) => void;
	mediaItem: MediaItem;
	uploadItem: File | undefined;
	onToggleKind: (kind: MediaItemKind) => void;
};

function FileItem({
	currentCoverSourceHash,
	onToggleCover,
	mediaItem,
	uploadItem,
	onToggleKind,
}: Props) {
	const { attributes, listeners, setNodeRef, transform, transition } =
		useSortable({ id: mediaItem.fileHash });

	const style = {
		transform: CSS.Transform.toString(transform),
		transition,
	};

	function getFileIcon(mediaType: string) {
		if (mediaType === "IMAGE") return <ImageIcon className="h-5 w-5" />;
		if (mediaType === "VIDEO") return <FileVideo className="h-5 w-5" />;
		if (mediaType === "AUDIO") return <FileAudio className="h-5 w-5" />;
		return <FileText className="h-5 w-5" />;
	}

	function formatFileSize(bytes: number | string): string {
		if (bytes === 0) return "0 Bytes";
		const k = 1024;
		const sizes = ["Bytes", "KB", "MB", "GB"];
		const i = Math.floor(Math.log(Number(bytes)) / Math.log(k));
		return `${Math.round((Number(bytes) / k ** i) * 100) / 100} ${sizes[i]}`;
	}

	const imgUrl = useMemo(
		() =>
			mediaItem.mediaType === "IMAGE" && uploadItem
				? URL.createObjectURL(uploadItem)
				: null,
		[uploadItem, mediaItem.mediaType],
	);

	return (
		<div ref={setNodeRef} style={style}>
			<Card className="p-3 bg-card hover:bg-accent/50 transition-colors">
				<div className="flex items-center gap-3">
					<div
						className="cursor-grab active:cursor-grabbing text-muted-foreground hover:text-foreground touch-none"
						{...attributes}
						{...listeners}
					>
						<GripVertical className="h-5 w-5" />
					</div>
					<div className="h-16 w-16 rounded border bg-muted flex items-center justify-center overflow-hidden shrink-0">
						{imgUrl ? (
							<img
								src={imgUrl}
								alt="Uploaded file"
								className="object-cover h-full w-full"
							/>
						) : (
							<div className="text-muted-foreground">
								{getFileIcon(mediaItem.mediaType)}
							</div>
						)}
					</div>

					<div className="flex-1 min-w-0">
						<p className="text-sm font-medium">#{mediaItem.order}</p>
						<p className="text-xs text-muted-foreground">
							{formatFileSize(mediaItem.fileSize)}
						</p>
					</div>

					<Button
						variant={
							currentCoverSourceHash === mediaItem.fileHash
								? "default"
								: "outline"
						}
						size="icon"
						onClick={() => onToggleCover(currentCoverSourceHash)}
						className="shrink-0"
						title={
							currentCoverSourceHash === mediaItem.fileHash
								? "Remove as cover"
								: "Set as cover"
						}
						disabled={mediaItem.fileHash === currentCoverSourceHash}
					>
						<Star
							className={`h-4 w-4 ${currentCoverSourceHash === mediaItem.fileHash ? "fill-current" : ""}`}
						/>
					</Button>

					<Select
						value={mediaItem.kind}
						onValueChange={(value) => onToggleKind(value as MediaItemKind)}
					>
						<SelectTrigger className="w-24">
							<SelectValue />
						</SelectTrigger>
						<SelectContent>
							<SelectItem value={"MAIN"}>Main</SelectItem>
							<SelectItem value={"EXTRA"}>Extra</SelectItem>
						</SelectContent>
					</Select>
				</div>
			</Card>
		</div>
	);
}

export default memo(FileItem);
