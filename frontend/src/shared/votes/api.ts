import apiFetch from "@/shared/http/fetch";

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

    // const result = (await response.json()) as RatingReasonGetAllActiveResult;
    // return result.reasons;
}