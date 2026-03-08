import apiFetch from "@/shared/http/fetch";
import {Leaderboard, LeaderboardType, Player} from "@/shared/players/types";

export async function getPlayer(nickname: string): Promise<Player> {
    const response = await apiFetch(`/bff/players/${nickname}/summary`, {
        method: "GET"
    });

    if (!response.ok) {
        throw new Error("Failed to load rating reasons");
    }

    const result = (await response.json()) as Player;
    return result;
}

export async function getLeaderboard(type: LeaderboardType, limit: number): Promise<Leaderboard> {
    const response = await apiFetch(`/bff/players/leaderboard?type=${type}&limit=${limit}`, {
        method: "GET"
    });

    if (!response.ok) {
        throw new Error("Failed to load rating reasons");
    }

    const result = (await response.json()) as Leaderboard;
    return result;
}