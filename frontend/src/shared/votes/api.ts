import apiFetch from "@/shared/http/fetch";
import { Comments } from "@/shared/votes/types";

export async function createVote(
  nickname: string,
  reasonId: string,
  comment: string,
): Promise<void> {
  const response = await apiFetch(`/bff/votes`, {
    method: "POST",
    body: JSON.stringify({
      nickname: nickname,
      reason_id: reasonId,
      comment: comment,
    }),
  });

  if (!response.ok) {
    throw new Error("Failed to create vote");
  }
}
