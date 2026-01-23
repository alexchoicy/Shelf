import type { components } from "@/data/APIschema";

type Props = {
	children?: React.ReactNode;
	user: components["schemas"]["UserDto"];
};

export default function Header({ user, children }: Props) {
	return <>{JSON.stringify(user)}</>;
}
