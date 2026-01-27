import type { components } from "@/data/APIschema";
import Form from "./Form";
import Preview from "./Preview";

type FormRequest = components["schemas"]["WorkCreationRequest"];
type PartyList = components["schemas"]["PartyListDto"];

type Props = {
	FormInfo: FormRequest;
	setFormData: (data: FormRequest) => void;
	uploadItems: Record<string, File>;
	partySearchList: PartyList[];
};

export default function LeftPanel({
	FormInfo,
	setFormData,
	uploadItems,
	partySearchList,
}: Props) {
	return (
		<div className="flex w-1/2 border-border border-r flex-col">
			<div className="border-b border-border px-6 py-4 z-10">
				<Preview
					FormInfo={FormInfo}
					uploadItems={uploadItems}
					partySearchList={partySearchList}
				/>
			</div>
			<div className="flex-1 flex-col gap-4 overflow-hidden">
				<Form
					FormInfo={FormInfo}
					setFormData={setFormData}
					partySearchList={partySearchList}
				/>
			</div>
		</div>
	);
}
