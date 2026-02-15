import { useQuery } from "@tanstack/react-query";
import { Link } from "@tanstack/react-router";
import { type LucideIcon, Plus, Settings } from "lucide-react";
import { meQueries } from "@/lib/queries/me.queries";
import type { FileRouteTypes } from "@/routeTree.gen";
import {
	Sidebar as ShadcnSidebar,
	SidebarContent,
	SidebarFooter,
	SidebarGroup,
	SidebarGroupContent,
	SidebarHeader,
	SidebarMenu,
	SidebarMenuButton,
	SidebarMenuItem,
	SidebarMenuSub,
	SidebarMenuSubButton,
	SidebarMenuSubItem,
} from "../shadcn/sidebar";

type SidebarOptions = {
	name: string;
	icon: LucideIcon;
	link: FileRouteTypes["to"];
	children?: SidebarOptions[];
};

export function Sidebar() {
	const { data, isLoading } = useQuery(meQueries.getMe());
	if (isLoading || !data) {
		return null;
	}

	const sidebarOptions: SidebarOptions[] = [
		{
			name: "Dashboard",
			icon: Settings,
			link: "/",
		},
	];

	return (
		<ShadcnSidebar>
			<SidebarHeader>
				<Link to="/">Shelf</Link>
			</SidebarHeader>
			<SidebarContent>
				<SidebarGroup>
					<SidebarMenu>
						{sidebarOptions.map((option) => (
							<SidebarMenuItem key={option.name}>
								<SidebarMenuButton
									render={
										<Link to={option.link}>
											<option.icon />
											<span>{option.name}</span>
										</Link>
									}
								></SidebarMenuButton>
								{option.children && (
									<SidebarMenuSub>
										{option.children?.map((child) => (
											<SidebarMenuSubItem key={child.name}>
												<SidebarMenuSubButton
													render={
														<Link to={child.link}>
															<child.icon />
															<span>{child.name}</span>
														</Link>
													}
												></SidebarMenuSubButton>
											</SidebarMenuSubItem>
										))}
									</SidebarMenuSub>
								)}
							</SidebarMenuItem>
						))}
					</SidebarMenu>
				</SidebarGroup>
				<SidebarGroup className="mt-auto">
					<SidebarGroupContent>
						<SidebarMenu>
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link to="/create">
											<div className="w-full flex justify-center items-center gap-2">
												<Plus />
												<span>New Work</span>
											</div>
										</Link>
									}
								></SidebarMenuButton>
							</SidebarMenuItem>
						</SidebarMenu>
					</SidebarGroupContent>
				</SidebarGroup>
			</SidebarContent>
			<SidebarFooter>
				<SidebarMenu>
					<SidebarMenuItem>
						<SidebarMenuButton>
							<Settings />
							<span>Settings</span>
						</SidebarMenuButton>
					</SidebarMenuItem>
				</SidebarMenu>
			</SidebarFooter>
		</ShadcnSidebar>
	);
}
