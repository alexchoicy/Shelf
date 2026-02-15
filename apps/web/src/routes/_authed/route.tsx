import { useQuery } from "@tanstack/react-query";
import { createFileRoute, Outlet } from "@tanstack/react-router";
import { Sidebar } from "@/components/ui/Sidebar";
import { SidebarProvider } from "@/components/shadcn/sidebar";
import { Toaster } from "@/components/shadcn/sonner";
import { TooltipProvider } from "@/components/shadcn/tooltip";
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

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error loading</div>;
	}

	return (
		<TooltipProvider>
			<SidebarProvider>
				<Sidebar />
				<Outlet />
				<Toaster />
			</SidebarProvider>
		</TooltipProvider>
	);
}
