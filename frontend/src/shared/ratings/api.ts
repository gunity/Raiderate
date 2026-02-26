import {RatingReason} from "@/shared/ratings/types";

type RatingReasonGetAllActiveResult = { reasons: RatingReason[] };

export async function getAllActiveRatingReasons(): Promise<RatingReason[]> {
    const response = await fetch(`/bff/rating-reasons/admin`, {
        method: "GET",
        cache: "no-store",
        credentials: "include"
    });

    if (!response.ok) {
        throw new Error("Failed to load admin rating reasons");
    }

    const result = (await response.json()) as RatingReasonGetAllActiveResult;
    return result.reasons;
}

type RatingReasonCreateResult = { id: number };

export async function createRatingReason(item: RatingReason): Promise<number> {
    const response = await fetch(`/bff/rating-reasons/admin`, {
        method: "POST",
        cache: "no-store",
        headers: {"content-type": "application/json"},
        credentials: "include",
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
    const response = await fetch(`/bff/rating-reasons/admin/${item.id}`, {
        method: "PUT",
        cache: "no-store",
        headers: {"content-type": "application/json"},
        credentials: "include",
        body: JSON.stringify({
            code: item.code,
            value: item.value,
            is_active: item.is_active,
        })
    });

    if (!response.ok) {
        throw new Error("Failed to update rating reason");
    }
}