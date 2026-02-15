import { useMutation, useQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { Save } from "lucide-react";
import { useState } from "react";
import { toast } from "sonner";
import LeftPanel from "@/components/create/LeftPanel";
import RightPanel from "@/components/create/RightPanel";
import { Button } from "@/components/shadcn/button";
import type { components } from "@/data/APIschema";
import { hashBlobStream } from "@/lib/fileHash";
import { partyQueries } from "@/lib/queries/party.queries";
import { workMutations } from "@/lib/queries/work.queries";

export const Route = createFileRoute("/_authed/create/")({
	component: RouteComponent,
	loader: async ({ context }) => {
		await context.queryClient.ensureQueryData(
			partyQueries.getPartySearchList(),
		);
	},
});

type FormRequest = components["schemas"]["WorkCreationRequest"];
type PartyList = components["schemas"]["PartyListDto"];

function RouteComponent() {
	const [formData, setFormData] = useState<FormRequest>({
		title: "",
		description: "",
		credits: [],
		isAI: false,
		medium: "IMAGE",
		type: "ILLUSTRATION",
		visibility: "PUBLIC",
		rating: "GENERAL",
		mediaItems: [],
	});

	const [uploadItems, setUploadItems] = useState<Record<string, File>>({});
	const [cover, setCover] = useState<Blob | null>(null);

	const [selectedArtistsCredit, setSelectedArtistsCredit] = useState<
		PartyList[]
	>([]);

	const { data, isLoading } = useQuery(partyQueries.getPartySearchList());

	const { mutateAsync } = useMutation(workMutations.create);

	if (isLoading || !data) {
		return <div>Loading...</div>;
	}

	const handleSave = async () => {
		if (!formData.title || formData.title.trim().length === 0) {
			toast.warning("Title is required");
			return;
		}

		if (selectedArtistsCredit.length === 0) {
			toast.warning("At least one credit is required");
			return;
		}

		if (!formData.mediaItems || formData.mediaItems.length === 0) {
			toast.warning("At least one media item is required");
			return;
		}

		const payload: FormRequest = {
			...formData,
			credits: selectedArtistsCredit.map((party) => ({
				partyId: party.partyId,
				role: "ARTIST",
			})),
		};

		if (cover) {
			payload.coverHash = await hashBlobStream(cover);
			payload.coverMimeType = cover.type;
			payload.coverWidth = 1920;
			payload.coverHeight = 1080;
		}

		try {
			const result = await mutateAsync(payload);
			toast.success("Work created successfully!");
			console.log("Created work:", result);
		} catch (error) {
			toast.error("Failed to create work");
			console.error("Error creating work:", error);
		}
	};

	return (
		<div className="flex w-full h-full flex-col">
			<header className="border-b bg-card sticky top-0 z-30">
				<div className=" px-4 py-4 flex items-center justify-between">
					<div>
						<h1 className="text-2xl font-semibold tracking-tight">
							Create Work
						</h1>
						<p className="text-sm text-muted-foreground">
							Add a new work to your library
						</p>
					</div>
					<Button onClick={handleSave} size="lg" className="gap-2">
						<Save className="h-4 w-4" />
						Save Work
					</Button>
				</div>
			</header>
			<div className="flex flex-1">
				<LeftPanel
					FormInfo={formData}
					setFormData={setFormData}
					uploadItems={uploadItems}
					partySearchList={data}
					cover={cover}
					setCover={setCover}
					selectedArtistsCredit={selectedArtistsCredit}
					setSelectedArtistsCredit={setSelectedArtistsCredit}
				/>
				<RightPanel
					FormInfo={formData}
					setFormData={setFormData}
					uploadItems={uploadItems}
					setUploadItems={setUploadItems}
					cover={cover}
					setCover={setCover}
				/>
			</div>
		</div>
	);
}
