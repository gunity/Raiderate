import { Comments } from "@/shared/votes/types";
import { env } from "@/shared/env";
import apiFetchServer from "@/shared/http/fetch.server";

export async function getCommentsServer(
  playerId: number,
  limit: number,
): Promise<Comments> {
  const response = await apiFetchServer(
    `${env.gatewayInternalUrl}/api/votes?playerId=${playerId}&limit=${limit}`,
    {
      method: "GET",
    },
  );

  if (!response.ok) {
    throw new Error("Failed to get comments");
  }

  return (await response.json()) as Comments;
}
