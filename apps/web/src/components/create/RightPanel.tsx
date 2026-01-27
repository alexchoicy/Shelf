import type { components } from "@/data/APIschema";
import FilesList from "./FilesList";

type FormRequest = components["schemas"]["WorkCreationRequest"];

type Props = {
	FormInfo: FormRequest;
	setFormData: (data: FormRequest) => void;
	uploadItems: Record<string, File>;
	setUploadItems: (items: Record<string, File>) => void;
};

export default function RightPanel({
	FormInfo,
	setFormData,
	uploadItems,
	setUploadItems,
}: Props) {
	return (
		<div className="flex w-1/2 flex-col bg-muted/20">
			<div className="border-b border-border px-6 py-4">
				<h2 className="font-medium text-foreground">Files</h2>
				<p className="text-xs text-muted-foreground mt-0.5">
					{Object.keys(uploadItems).length} items
				</p>
			</div>
			<div className="flex-1 flex-col gap-4 overflow-hidden">
				<FilesList
					FormInfo={FormInfo}
					setFormData={setFormData}
					uploadItems={uploadItems}
					setUploadItems={setUploadItems}
				/>
			</div>
		</div>
	);
}
