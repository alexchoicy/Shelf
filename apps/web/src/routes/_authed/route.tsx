import { useQuery } from "@tanstack/react-query";
import { createFileRoute, Outlet } from "@tanstack/react-router";
import Header from "@/components/Header";
import { authQueries } from "@/lib/queries/auth.queries";
import { meQueries } from "@/lib/queries/me.queries";

export const Route = createFileRoute("/_authed")({
	component: RouteComponent,
	loader: async ({ context }) => {
		await context.queryClient.ensureQueryData(authQueries.checkAuth());
		await context.queryClient.ensureQueryData(meQueries.getMe());
	},
});

function RouteComponent() {
	const { isLoading, isError } = useQuery(authQueries.checkAuth());
	const { data } = useQuery(meQueries.getMe());

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error loading</div>;
	}

	return (
		<>
			<Header user={data || { id: "", userName: "", roles: [] }} />
			<Outlet />
		</>
	);
}
