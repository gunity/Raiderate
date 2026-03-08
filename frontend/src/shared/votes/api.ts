import apiFetch from "@/shared/http/fetch";
import {VoteComments} from "@/shared/votes/types";

export async function createVote(nickname: string, reasonId: number, comment: string): Promise<void> {
    const response = await apiFetch(`/bff/votes`, {
        method: "POST",
        body: JSON.stringify({
            nickname: nickname,
            reason_id: reasonId,
            comment: comment
        }),
    });

    if (!response.ok) {
        throw new Error("Failed to create vote");
    }
}

export async function getComments(playerId: number, limit: number): Promise<VoteComments> {
    const response = await apiFetch(`/bff/votes?playerId=${playerId}&limit=${limit}`, {
        method: "GET"
    });

    if (!response.ok) {
        throw new Error("Failed to update rating reason");
    }

    return (await response.json()) as VoteComments;
}