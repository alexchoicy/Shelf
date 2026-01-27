import { useQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";
import LeftPanel from "@/components/create/LeftPanel";
import RightPanel from "@/components/create/RightPanel";
import type { components } from "@/data/APIschema";
import { partyQueries } from "@/lib/queries/party.queries";

export const Route = createFileRoute("/_authed/create/")({
	component: RouteComponent,
	loader: async ({ context }) => {
		await context.queryClient.ensureQueryData(
			partyQueries.getPartySearchList(),
		);
	},
});

type FormRequest = components["schemas"]["WorkCreationRequest"];

function RouteComponent() {
	const [formData, setFormData] = useState<FormRequest>({
		title: "",
		description: "",
		primaryPartyId: "",
		isAI: false,
		medium: "IMAGE",
		type: "ILLUSTRATION",
		visibility: "PUBLIC",
		rating: "GENERAL",
		coverHash: "",
		mediaItems: [],
	});

	const [uploadItems, setUploadItems] = useState<Record<string, File>>({});

	const { data, isLoading } = useQuery(partyQueries.getPartySearchList());

	if (isLoading || !data) {
		return <div>Loading...</div>;
	}

	return (
		<div className="flex flex-1 h-screen">
			<LeftPanel
				FormInfo={formData}
				setFormData={setFormData}
				uploadItems={uploadItems}
				partySearchList={data}
			/>
			<RightPanel
				FormInfo={formData}
				setFormData={setFormData}
				uploadItems={uploadItems}
				setUploadItems={setUploadItems}
			/>
		</div>
	);
}
