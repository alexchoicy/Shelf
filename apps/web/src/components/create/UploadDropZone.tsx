import { useState } from "react";
import { useDropzone } from "react-dropzone";
import type { components } from "@/data/APIschema";
import { hashFileStream } from "@/lib/fileHash";
import { cropTo16x9 } from "@/lib/imageCrop";
import { cn } from "@/lib/utils";

type FormRequest = components["schemas"]["WorkCreationRequest"];

type Props = {
	FormInfo: FormRequest;
	setFormData: (data: FormRequest) => void;
	uploadItems: Record<string, File>;
	setUploadItems: (items: Record<string, File>) => void;
	cover: Blob | null;
	setCover: (cover: Blob | null) => void;
	currentCoverSourceHash: string;
	setCurrentCoverSourceHash: (hash: string) => void;
};

export default function UploadDropZone({
	FormInfo,
	setFormData,
	uploadItems,
	setUploadItems,
	cover,
	setCover,
	setCurrentCoverSourceHash,
}: Props) {
	const [isProcessing, setIsProcessing] = useState(false);

	async function onDrop(acceptedFiles: File[]) {
		setIsProcessing(true);
		try {
			const newItems: Record<string, File> = { ...uploadItems };
			const newMediaItems: components["schemas"]["MediaItemCreationRequest"][] =
				[];
			const orderBase = FormInfo.mediaItems.length;

			for (const [, file] of acceptedFiles.entries()) {
				const hash = await hashFileStream(file);
				if (!newItems[hash]) {
					newMediaItems.push({
						fileHash: hash,
						mediaType: "IMAGE",
						mimeType: file.type,
						fileSize: file.size,
						order: orderBase + newMediaItems.length + 1,
						kind: "MAIN",
					});
					newItems[hash] = file;
				}
				//TODO: handle thumbnails here
			}

			if (newMediaItems.length > 0) {
				setFormData({
					...FormInfo,
					mediaItems: [...FormInfo.mediaItems, ...newMediaItems],
				});
			}

			const firstFileHash = Object.keys(newItems)[0];
			const firstFile = newItems[firstFileHash];
			// only set cover if it is image. Generated cover from video in backend.
			if (!cover && firstFile.type.startsWith("image/")) {
				try {
					const croppedBlob = await cropTo16x9(firstFile);
					setCover(croppedBlob);
				} catch (error) {
					console.error("Failed to crop image:", error);
				}

				setCurrentCoverSourceHash(firstFileHash);
			}

			setUploadItems(newItems);
		} catch (error) {
			console.error("Error processing files:", error);
		} finally {
			setIsProcessing(false);
		}
	}

	const { getRootProps, getInputProps, isFocused, isDragAccept } = useDropzone({
		accept: { "image/*": [] },
		onDrop,
		disabled: isProcessing,
	});

	return (
		<div
			{...getRootProps()}
			className={cn(
				"relative flex min-h-40 cursor-pointer flex-col items-center justify-center rounded-xl border-2 border-dashed transition-all",
				isProcessing && "opacity-50 cursor-not-allowed pointer-events-none",
				!isProcessing && (isFocused || isDragAccept)
					? "border-primary bg-primary/10 scale-[1.02]"
					: "border-border hover:border-primary/50 hover:bg-muted/50",
			)}
		>
			<input {...getInputProps()} />
			<p className="">
				{isProcessing
					? "Processing files..."
					: "Drag & drop files here, or click to select files"}
			</p>
		</div>
	);
}
