import { Leaderboard, LeaderboardType, Player } from "@/shared/players/types";
import { env } from "@/shared/env";
import apiFetchServer from "@/shared/http/fetch.server";
import apiFetch from "@/shared/http/fetch";

export async function getPlayerServer(nickname: string): Promise<Player> {
  const response = await apiFetchServer(
    `${env.gatewayInternalUrl}/api/players/${nickname}/summary`,
    {
      method: "GET",
    },
  );

  if (!response.ok) {
    throw new Error("Failed to load player summary");
  }

  const result = (await response.json()) as Player;
  return result;
}

export async function getLeaderboardServer(
  type: LeaderboardType,
  limit: number,
): Promise<Leaderboard> {
  const response = await apiFetch(
    `${env.gatewayInternalUrl}/api/players/leaderboard?type=${type}&limit=${limit}`,
    {
      method: "GET",
    },
  );

  if (!response.ok) {
    throw new Error("Failed to load rating reasons");
  }

  const result = (await response.json()) as Leaderboard;
  return result;
}
