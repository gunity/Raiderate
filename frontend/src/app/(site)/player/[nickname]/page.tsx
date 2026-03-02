import PlayerPage from "@/pages/PlayerPage";

type Props = {
    params: Promise<{ nickname: string }>;
};

export default async function Page({params}: Props) {
    const {nickname} = await params;
    return <PlayerPage nickname={nickname}/>;
}