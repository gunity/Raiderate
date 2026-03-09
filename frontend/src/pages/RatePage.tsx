"use client";

import React, { useState } from "react";
import { RatingReason } from "@/shared/ratings/types";
import { getRatingReasonLabel } from "@/shared/ratings/reasons";
import { createVote } from "@/shared/votes/api";
import { Star } from "lucide-react";
import { useRouter } from "next/navigation";

type Props = {
  reasons: RatingReason[];
};

export default function RatePage({ reasons }: Props) {
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [sending, setSending] = useState<boolean>(false);
  const [nickname, setNickname] = useState("");
  const [comment, setComment] = useState("");
  const [reasonId, setReasonId] = useState<number | null>(null);

  const router = useRouter();

  async function handleSubmit() {
    setSending(true);
    if (reasonId == null) {
      setError("Reason is required");
      return;
    }

    try {
      await createVote(nickname, reasonId, comment);
      router.push(`/player/${nickname}`);
    } catch (error: unknown) {
      setError("Failed to create vote");
    } finally {
      setSending(false);
    }
  }

  return (
    <div className="flex min-h-[calc(100vh-57px)] flex-col items-center justify-center">
      <form className="flex w-72 flex-col gap-2" action={handleSubmit}>
        <input
          className="rounded border border-[#1d2226] p-2"
          type="text"
          placeholder="Nickname"
          value={nickname}
          onChange={(e) => setNickname(e.target.value)}
        />
        <select
          className="rounded border border-[#1d2226] p-2"
          value={reasonId ?? ""}
          onChange={(e) => setReasonId(Number(e.target.value))}
        >
          {reasons.map((reason) => (
            <option key={reason.id} value={reason.id}>
              {getRatingReasonLabel(reason.code)}
            </option>
          ))}
        </select>
        <input
          className="rounded border border-[#1d2226] p-2"
          type="text"
          placeholder="Comment (optional)"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
        />

        {!!error && <div className="text-center text-[#f4101b]">{error}</div>}

        <button
          type="submit"
          className="cursor-pointer pt-3"
          disabled={sending}
        >
          {!sending ? (
            <div className="inline-flex items-center gap-2">
              <Star className="h-5 w-5" />
              <span>Send</span>
            </div>
          ) : (
            "sending..."
          )}
        </button>
      </form>
    </div>
  );
}
