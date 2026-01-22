import { useQuery } from "@tanstack/react-query";
import { createFileRoute, Outlet } from "@tanstack/react-router";
import Header from "@/components/Header";
import { authQueries } from "@/lib/queries/auth.queries";

export const Route = createFileRoute("/_authed")({
	component: RouteComponent,
});

function RouteComponent() {
	const { isLoading, isError } = useQuery(authQueries.me());

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error loading user data.</div>;
	}

	return (
		<>
			<Header />
			<Outlet />
		</>
	);
}
