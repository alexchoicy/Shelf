import { createFileRoute } from "@tanstack/react-router";
import CharacterCombobox from "@/components/create/combobox/characterCombobox";
import PartyCombobox from "@/components/create/combobox/partyCombobox";
import { AppLayout } from "@/components/ui/appLayout";
import { characterQueries } from "@/lib/queries/character.queries";
import { partyQueries } from "@/lib/queries/party.queries";

export const Route = createFileRoute("/_authed/create/")({
	component: RouteComponent,
	loader: ({ context }) => {
		context.queryClient.ensureQueryData(
			characterQueries.getCharacterSearchList(),
		);
		context.queryClient.ensureQueryData(partyQueries.getPartySearchList());
	},
});

function RouteComponent() {
	return (
		<AppLayout>
			<CharacterCombobox />
			<PartyCombobox />
		</AppLayout>
	);
}
