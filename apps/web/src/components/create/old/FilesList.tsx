import {
	closestCenter,
	DndContext,
	type DragEndEvent,
	KeyboardSensor,
	PointerSensor,
	useSensor,
	useSensors,
} from "@dnd-kit/core";
import {
	arrayMove,
	SortableContext,
	sortableKeyboardCoordinates,
	verticalListSortingStrategy,
} from "@dnd-kit/sortable";
import { useCallback, useMemo, useState } from "react";
import type { components } from "@/data/APIschema";
import { cropTo16x9 } from "@/lib/imageCrop";
import { Separator } from "../shadcn/separator";
import FileItem from "./FileItem";
import UploadDropZone from "./UploadDropZone";

type FormRequest = components["schemas"]["WorkCreationRequest"];
type MediaItemKind = components["schemas"]["MediaItemKind"];

type Props = {
	FormInfo: FormRequest;
	setFormData: (data: FormRequest) => void;
	uploadItems: Record<string, File>;
	setUploadItems: (items: Record<string, File>) => void;
	cover: Blob | null;
	setCover: (cover: Blob | null) => void;
};

//TODO: on server side, it will reorder again, "Extra" will be in the end of ordering.
function normalizeOrders(
	items: components["schemas"]["MediaItemCreationRequest"][],
) {
	return items.map((it, idx) => ({ ...it, order: idx + 1 }));
}

export default function FilesList({
	FormInfo,
	setFormData,
	uploadItems,
	setUploadItems,
	cover,
	setCover,
}: Props) {
	const [currentCoverSourceHash, setCurrentCoverSourceHash] =
		useState<string>("");

	const sensors = useSensors(
		useSensor(PointerSensor),
		useSensor(KeyboardSensor, {
			coordinateGetter: sortableKeyboardCoordinates,
		}),
	);

	const itemIds = useMemo(
		() => FormInfo.mediaItems.map((item) => item.fileHash),
		[FormInfo.mediaItems],
	);

	const onDragEnd = (event: DragEndEvent) => {
		const { active, over } = event;
		if (!over) return;
		if (active.id === over.id) return;

		const oldIndex = FormInfo.mediaItems.findIndex(
			(item) => item.fileHash === active.id,
		);
		const newIndex = FormInfo.mediaItems.findIndex(
			(item) => item.fileHash === over.id,
		);
		if (oldIndex === -1 || newIndex === -1) return;

		const newMediaItems = arrayMove(FormInfo.mediaItems, oldIndex, newIndex);
		const normalizedMediaItems = normalizeOrders(newMediaItems);

		setFormData({
			...FormInfo,
			mediaItems: normalizedMediaItems,
		});
	};

	const handleToggleCover = useCallback(
		async (fileHash: string) => {
			setCurrentCoverSourceHash(fileHash);
			const file = uploadItems[fileHash];
			if (file) {
				try {
					const croppedBlob = await cropTo16x9(file);
					setCover(croppedBlob);
				} catch (error) {
					console.error("Failed to crop image:", error);
				}
			}
		},
		[uploadItems, setCover],
	);

	const handleToggleKind = useCallback(
		(fileHash: string, kind: MediaItemKind) => {
			const newMediaItems = FormInfo.mediaItems.map((mi) =>
				mi.fileHash === fileHash ? { ...mi, kind } : mi,
			);
			setFormData({
				...FormInfo,
				mediaItems: newMediaItems,
			});
		},
		[FormInfo, setFormData],
	);

	// The type errors is fine
	return (
		<div className="flex flex-col h-full p-5 space-y-5">
			<UploadDropZone
				FormInfo={FormInfo}
				setFormData={setFormData}
				uploadItems={uploadItems}
				setUploadItems={setUploadItems}
				cover={cover}
				setCover={setCover}
				currentCoverSourceHash={currentCoverSourceHash}
				setCurrentCoverSourceHash={setCurrentCoverSourceHash}
			/>
			<Separator />
			<DndContext
				sensors={sensors}
				collisionDetection={closestCenter}
				onDragEnd={onDragEnd}
			>
				<SortableContext items={itemIds} strategy={verticalListSortingStrategy}>
					<div className="flex-1 space-y-3 overflow-x-hidden overflow-y-auto p-2">
						{FormInfo.mediaItems.map((item) => (
							<FileItem
								key={item.fileHash}
								mediaItem={item}
								uploadItem={uploadItems[item.fileHash]}
								currentCoverSourceHash={currentCoverSourceHash}
								onToggleCover={handleToggleCover}
								onToggleKind={handleToggleKind}
							/>
						))}
					</div>
				</SortableContext>
			</DndContext>
		</div>
	);
}
