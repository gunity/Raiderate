import PlayerPage from "@/pages/PlayerPage";
import { getPlayerServer } from "@/shared/players/api.server";
import { getCommentsServer } from "@/shared/votes/api.server";
import { notFound } from "next/navigation";

type Props = {
  params: Promise<{ nickname: string }>;
};

export default async function Page({ params }: Props) {
  const { nickname } = await params;

  const player = await getPlayerServer(nickname);
  if (!player) {
    notFound();
  }

  const comments = await getCommentsServer(player.id, 10);

  return <PlayerPage player={player} comments={comments.items} />;
}
