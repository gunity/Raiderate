import { RatingReason } from "@/shared/ratings/types";
import apiFetch from "@/shared/http/fetch";

type RatingReasonGetAllResult = { reasons: RatingReason[] };

export async function getAllRatingReasons(): Promise<RatingReason[]> {
  const response = await apiFetch(`/bff/rating-reasons/admin`, {
    method: "GET",
  });

  if (!response.ok) {
    throw new Error("Failed to load rating reasons");
  }

  const result = (await response.json()) as RatingReasonGetAllResult;
  return result.reasons;
}

type RatingReasonCreateResult = { id: number };

export async function createRatingReason(item: RatingReason): Promise<number> {
  const response = await apiFetch(`/bff/rating-reasons/admin`, {
    method: "POST",
    body: JSON.stringify({
      code: item.code,
      value: item.value,
      is_active: item.is_active,
    }),
  });

  if (!response.ok) {
    throw new Error("Failed to create rating reason");
  }

  const result = (await response.json()) as RatingReasonCreateResult;
  return result.id;
}

export async function updateRatingReason(item: RatingReason): Promise<void> {
  const response = await apiFetch(`/bff/rating-reasons/admin/${item.id}`, {
    method: "PUT",
    body: JSON.stringify({
      code: item.code,
      value: item.value,
      is_active: item.is_active,
    }),
  });

  if (!response.ok) {
    throw new Error("Failed to update rating reason");
  }
}
