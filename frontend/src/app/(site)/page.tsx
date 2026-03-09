import HomePage from "@/pages/HomePage";
import { getLeaderboardServer } from "@/shared/players/api.server";

export default async function Page() {
  const topLeaderboard = await getLeaderboardServer("Top", 5);
  const bottomLeaderboard = await getLeaderboardServer("Bottom", 5);

  return (
    <HomePage
      topLeaderboard={topLeaderboard}
      bottomLeaderboard={bottomLeaderboard}
    />
  );
}
