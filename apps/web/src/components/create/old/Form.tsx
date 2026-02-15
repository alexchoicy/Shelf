import { format } from "date-fns";
import { useId, useState } from "react";
import type { components } from "@/data/APIschema";
import {
	getWorkTypesByMedium,
	WORK_MEDIUM_OPTIONS,
	WORK_RATING_OPTIONS,
	WORK_VISIBILITY_OPTIONS,
} from "@/enums/WorkEnums";
import { Button } from "../shadcn/button";
import { Calendar } from "../shadcn/calendar";
import {
	Field,
	FieldContent,
	FieldGroup,
	FieldLabel,
	FieldLegend,
	FieldSeparator,
	FieldSet,
} from "../shadcn/field";
import { Input } from "../shadcn/input";
import { Popover, PopoverContent, PopoverTrigger } from "../shadcn/popover";
import {
	Select,
	SelectContent,
	SelectItem,
	SelectTrigger,
	SelectValue,
} from "../shadcn/select";
import { Switch } from "../shadcn/switch";
import { Textarea } from "../shadcn/textarea";
import PartyCombobox from "./partyCombobox";

type FormRequest = components["schemas"]["WorkCreationRequest"];
type PartyList = components["schemas"]["PartyListDto"];

type Props = {
	FormInfo: FormRequest;
	setFormData: (data: FormRequest) => void;
	partySearchList: PartyList[];
	selectedArtistsCredit: PartyList[];
	setSelectedArtistsCredit: React.Dispatch<React.SetStateAction<PartyList[]>>;
};

export default function Form({
	FormInfo,
	setFormData,
	partySearchList,
	selectedArtistsCredit,
	setSelectedArtistsCredit,
}: Props) {
	const title = useId();
	const workType = useId();
	const medium = useId();
	const rating = useId();
	const visibility = useId();
	const isAI = useId();
	const description = useId();
	const releasedAt = useId();

	const [date, setDate] = useState<Date>();

	return (
		<div className="w-full h-full p-8 overflow-y-auto">
			<FieldGroup>
				<FieldSet>
					<FieldLegend>Work MetaData</FieldLegend>
					<FieldGroup>
						<Field>
							<FieldLabel htmlFor={title}>Title</FieldLabel>
							<Input
								id={title}
								required
								value={FormInfo.title}
								onChange={(e) =>
									setFormData({ ...FormInfo, title: e.target.value })
								}
							/>
						</Field>
						<Field>
							<FieldLabel htmlFor={description}>Description</FieldLabel>
							<Textarea
								id={description}
								value={FormInfo.description || ""}
								onChange={(e) =>
									setFormData({ ...FormInfo, description: e.target.value })
								}
							/>
						</Field>
						<Field>
							<FieldLabel htmlFor={releasedAt}>Release Date</FieldLabel>
							<Popover>
								<PopoverTrigger asChild>
									<Button
										variant="outline"
										id={releasedAt}
										className="justify-start font-normal"
									>
										{" "}
										{FormInfo.releasedAt ? (
											format(FormInfo.releasedAt, "PPP")
										) : (
											<span>Pick a date Or Ignore</span>
										)}
									</Button>
								</PopoverTrigger>
								<PopoverContent className="w-auto p-0" align="start">
									<Calendar
										mode="single"
										selected={date}
										onSelect={(date) => {
											setDate(date);
											setFormData({
												...FormInfo,
												releasedAt: date ? date.toISOString() : null,
											});
										}}
										defaultMonth={date || new Date()}
									/>
								</PopoverContent>
							</Popover>
						</Field>
					</FieldGroup>
				</FieldSet>
				<FieldSeparator />
				<FieldSet>
					<FieldLegend>Credits</FieldLegend>
					<FieldGroup>
						<Field>
							<FieldLabel>Artists</FieldLabel>
							<PartyCombobox
								parties={partySearchList}
								selectedValues={selectedArtistsCredit}
								setSelectedValues={setSelectedArtistsCredit}
							/>
						</Field>
					</FieldGroup>
				</FieldSet>
				<FieldSeparator />
				<FieldSet>
					<FieldLegend>Work Type</FieldLegend>
					<FieldGroup>
						<div className="grid grid-cols-2 gap-4">
							<Field>
								<FieldLabel htmlFor={medium}>Medium</FieldLabel>
								<Select
									defaultValue="IMAGE"
									onValueChange={(value) => {
										const newMedium =
											value as components["schemas"]["WorkMedium"];
										const validTypes = getWorkTypesByMedium(newMedium);
										const firstValidType = validTypes[0]?.value;
										setFormData({
											...FormInfo,
											medium: newMedium,
											type: firstValidType as components["schemas"]["WorkType"],
										});
									}}
								>
									<SelectTrigger>
										<SelectValue id={medium} />
									</SelectTrigger>
									<SelectContent>
										{WORK_MEDIUM_OPTIONS.map((option) => (
											<SelectItem key={option.value} value={option.value}>
												{option.label}
											</SelectItem>
										))}
									</SelectContent>
								</Select>
							</Field>

							<Field>
								<FieldLabel htmlFor={workType}>Type</FieldLabel>
								<Select
									value={FormInfo.type || "ILLUSTRATION"}
									onValueChange={(value) =>
										setFormData({
											...FormInfo,
											type: value as components["schemas"]["WorkType"],
										})
									}
								>
									<SelectTrigger>
										<SelectValue id={workType} />
									</SelectTrigger>
									<SelectContent>
										{getWorkTypesByMedium(
											(FormInfo.medium as components["schemas"]["WorkMedium"]) ||
												"IMAGE",
										).map((option) => (
											<SelectItem key={option.value} value={option.value}>
												{option.label}
											</SelectItem>
										))}
									</SelectContent>
								</Select>
							</Field>

							<Field>
								<FieldLabel htmlFor={rating}>Rating</FieldLabel>
								<Select
									defaultValue="GENERAL"
									onValueChange={(value) =>
										setFormData({
											...FormInfo,
											rating: value as components["schemas"]["WorkRating"],
										})
									}
								>
									<SelectTrigger>
										<SelectValue id={rating} />
									</SelectTrigger>
									<SelectContent>
										{WORK_RATING_OPTIONS.map((option) => (
											<SelectItem key={option.value} value={option.value}>
												{option.label}
											</SelectItem>
										))}
									</SelectContent>
								</Select>
							</Field>

							<Field>
								<FieldLabel htmlFor={visibility}>Visibility</FieldLabel>
								<Select
									defaultValue="PUBLIC"
									onValueChange={(value) =>
										setFormData({
											...FormInfo,
											visibility:
												value as components["schemas"]["WorkVisibility"],
										})
									}
								>
									<SelectTrigger>
										<SelectValue id={visibility} />
									</SelectTrigger>
									<SelectContent>
										{WORK_VISIBILITY_OPTIONS.map((option) => (
											<SelectItem key={option.value} value={option.value}>
												{option.label}
											</SelectItem>
										))}
									</SelectContent>
								</Select>
							</Field>
						</div>
						<Field orientation="horizontal">
							<FieldContent>
								<FieldLabel htmlFor={isAI}>AI Generated</FieldLabel>
							</FieldContent>
							<Switch
								id={isAI}
								checked={FormInfo.isAI}
								onCheckedChange={(checked) =>
									setFormData({
										...FormInfo,
										isAI: checked,
									})
								}
							/>
						</Field>
					</FieldGroup>
				</FieldSet>
				<FieldSeparator />
			</FieldGroup>
		</div>
	);
}
