"use client";

import React, {useEffect, useState} from "react";
import {RatingReason} from "@/shared/ratings/types";
import {getAllActiveRatingReasons} from "@/shared/ratings/api";
import {getRatingReasonLabel} from "@/shared/ratings/reasons";
import {createVote} from "@/shared/votes/api";
import {Star} from "lucide-react";

export default function RatePage() {

    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [sending, setSending] = useState<boolean>(false);
    const [nickname, setNickname] = useState("");
    const [comment, setComment] = useState("");
    const [reasons, setReasons] = useState<RatingReason[]>([]);
    const [reasonId, setReasonId] = useState<number | null>(null);

    useEffect(() => {
        (async () => {
            try {
                const items = await getAllActiveRatingReasons();
                setReasons(items);
                if (items.length > 0) {
                    setReasonId(items[0].id!);
                }
            } catch (error: unknown) {
                setError("Failed to load rating reasons");
            }
        })();
    }, []);

    async function handleSubmit() {
        if (reasonId == null) {
            setError("Reason is required");
            return;
        }

        try {
            await createVote(nickname, reasonId, comment);
        } catch (error: unknown) {
            setError("Failed to create vote");
        }
    }

    return (
        <div className="min-h-[calc(100vh-57px)] flex flex-col items-center justify-center">
            <form className="flex flex-col gap-2 w-72" action={handleSubmit}>
                <input
                    className="border border-[#1d2226] rounded p-2"
                    type="text"
                    placeholder="Nickname"
                    value={nickname}
                    onChange={(e) => setNickname(e.target.value)}
                />
                <select
                    className="border border-[#1d2226] rounded p-2"
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
                    className="border border-[#1d2226] rounded p-2"
                    type="text"
                    placeholder="Comment (optional)"
                    value={comment}
                    onChange={(e) => setComment(e.target.value)}
                />

                {!!error && (
                    <div className="text-center text-[#f4101b]">{error}</div>
                )}

                <button
                    type="submit"
                    className="pt-3 cursor-pointer"
                    disabled={sending}
                >
                    {!sending ? (
                        <div className="inline-flex gap-2 items-center">
                            <Star className="w-5 h-5"/>
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