import { RatingReason } from "@/shared/ratings/types";
import { env } from "@/shared/env";
import apiFetchServer from "@/shared/http/fetch.server";

type RatingReasonGetAllActiveResult = { reasons: RatingReason[] };

export async function getAllActiveRatingReasonsServer(): Promise<
  RatingReason[]
> {
  const response = await apiFetchServer(
    `${env.gatewayInternalUrl}/api/rating-reasons`,
    {
      method: "GET",
    },
  );

  if (!response.ok) {
    throw new Error("Failed to load all active rating reasons");
  }

  const result = (await response.json()) as RatingReasonGetAllActiveResult;
  return result.reasons;
}

type RatingReasonGetAllResult = { reasons: RatingReason[] };

export async function getAllRatingReasonsServer(): Promise<RatingReason[]> {
  const response = await apiFetchServer(
    `${env.gatewayInternalUrl}/api/rating-reasons/admin`,
    {
      method: "GET",
    },
  );

  if (!response.ok) {
    const text = await response.text();
    throw new Error(
      `Failed to load all rating reasons: ${response.status} ${response.statusText} ${text}`,
    );
  }

  const result = (await response.json()) as RatingReasonGetAllResult;
  return result.reasons;
}
