"use client";

import {useEffect, useState} from "react";
import {getPlayer} from "@/shared/players/api";
import {Player} from "@/shared/players/types";

export default function PlayerPage({nickname}: { nickname: string }) {

    const [player, setPlayer] = useState<Player | null>(null);

    useEffect(() => {
        (async () => {
            const player = await getPlayer(nickname);
            setPlayer(player);
        })();
    }, [nickname]);

    return (
        <div>
            <label>id:{player?.id}</label>
            <label>nickname:{player?.nickname}</label>
            <label>rating:{player?.rating}</label>
            <label>votes_count:{player?.votes_count}</label>
        </div>
    );
}