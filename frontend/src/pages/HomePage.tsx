"use client";

import Link from "next/link";
import {Star} from "lucide-react";
import {useEffect, useState} from "react";
import {Leaderboard} from "@/shared/players/types";
import {getLeaderboard} from "@/shared/players/api";
import LeaderboardView from "@/components/Leaderboard";

export default function HomePage() {

    const [topLeaderboard, setTopLeaderboard] = useState<Leaderboard | null>(null);
    const [bottomLeaderboard, setBottomLeaderboard] = useState<Leaderboard | null>(null);

    useEffect(() => {
        (async () => {
            const topLeaderboard = await getLeaderboard("Top", 5);
            setTopLeaderboard(topLeaderboard);
            console.log("top", topLeaderboard);

            const bottomLeaderboard = await getLeaderboard("Bottom", 5);
            setBottomLeaderboard(bottomLeaderboard);
            console.log("bottom", bottomLeaderboard);
        })();
    }, []);

    return (
        <div className="mx-auto flex flex-col justify-center p-6 text-center max-w-xl">
            <span>
                Raiderate is a reputation platform for ARC Raiders players. It allows users to check
                player profiles, view ratings, read feedback from other players, and leave reviews
                based on real in-game experience. The platform also helps report toxic,
                unfair, or suspicious behavior, giving the community more transparency and accountability.
            </span>
            <Link
                href="/rate"
                className="inline-flex p-2 my-5 gap-2 justify-center border rounded"
            >
                <Star className="w-5 h-5"/>
                <span>
                    Rate player
                </span>
            </Link>
            <span className="text-[#2afe7f]">Highest Reputation</span>
            {topLeaderboard && (
                <LeaderboardView leaderboard={topLeaderboard}/>
            )}
            <span className="text-[#fdd333]">Lowest Reputation</span>
            {bottomLeaderboard && (
                <LeaderboardView leaderboard={bottomLeaderboard}/>
            )}
        </div>
    );
}

