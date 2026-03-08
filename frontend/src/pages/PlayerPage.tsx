"use client";

import {useEffect, useState} from "react";
import {getPlayer} from "@/shared/players/api";
import {Player} from "@/shared/players/types";
import {VoteComments} from "@/shared/votes/types";
import {getComments} from "@/shared/votes/api";

export default function PlayerPage({nickname}: { nickname: string }) {

    const [player, setPlayer] = useState<Player | null>(null);
    const [comments, setComments] = useState<VoteComments | null>(null);

    useEffect(() => {
        (async () => {
            const player = await getPlayer(nickname);
            setPlayer(player);

            const commentsResult = await getComments(player.id, 10);
            setComments(commentsResult);
        })();
    }, [nickname]);

    return (
        <div className="mx-auto w-full max-w-md px-6 py-6 grid gap-6">
            {/* header info */}
            <div className="grid gap-0">
                <div className="text-xs text-gray-500">nickname</div>
                <div className="font-mono text-2xl">{player?.nickname ?? "-"}</div>

                <div className="flex items-center justify-between pt-3">
                    <div>
                        <div className="text-xs text-gray-500">reputation</div>
                        <div
                            className={[
                                "font-mono text-2xl font-bold",
                                (player?.rating ?? 0) > 0 ? "text-[#2afe7f]" : "",
                                (player?.rating ?? 0) < 0 ? "text-[#fdd333]" : "",
                                (player?.rating ?? 0) === 0 ? "text-gray-200" : "",
                            ].join(" ")}
                        >
                            {(player?.rating ?? 0) > 0 ? `+${player?.rating}` : player?.rating ?? 0}
                        </div>
                    </div>

                    <div className="text-right">
                        <div className="text-xs text-gray-500">votes</div>
                        <div className="font-mono text-2xl font-bold">{player?.votes_count ?? 0}</div>
                    </div>
                </div>

                <div className="text-xs text-gray-500 pt-3">comments</div>
                {comments?.items?.length ? (
                    <div className="grid gap-2">
                        {comments.items.map((item, i) => (
                            <div key={i} className="text-sm">
                                {item.text}
                            </div>
                        ))}
                    </div>
                ) : (
                    <div className="text-sm text-gray-500">No comments yet</div>
                )}
            </div>
        </div>
    );
}