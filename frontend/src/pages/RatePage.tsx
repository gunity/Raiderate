"use client";

import {useEffect, useState} from "react";
import {RatingReason} from "@/shared/ratings/types";
import {getAllActiveRatingReasons} from "@/shared/ratings/api";
import {getRatingReasonLabel} from "@/shared/ratings/reasons";
import {createVote} from "@/shared/votes/api";

export default function RatePage() {

    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
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
        <div className="p-4">
            <form className="flex flex-col gap-2" action={handleSubmit}>
                <input
                    className="border rounded p-2 h-10 w-80"
                    type="text"
                    placeholder="Nickname"
                    value={nickname}
                    onChange={(e) => setNickname(e.target.value)}
                />
                <select
                    className="border rounded p-2 h-10 w-80"
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
                    className="border rounded p-2 h-10 w-80"
                    type="text"
                    placeholder="Comment (optional)"
                    value={comment}
                    onChange={(e) => setComment(e.target.value)}
                />

                {!!error && (
                    <div>{error}</div>
                )}

                <button
                    className="border rounded p-2 h-10 w-80"
                    type="submit"
                >
                    Send
                </button>
            </form>
        </div>
    );
}