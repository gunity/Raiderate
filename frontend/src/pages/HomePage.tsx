"use client";

import Link from "next/link";
import { Star } from "lucide-react";
import { Leaderboard } from "@/shared/players/types";
import LeaderboardView from "@/components/Leaderboard";

type Props = {
  topLeaderboard: Leaderboard;
  bottomLeaderboard: Leaderboard;
};

export default function HomePage({ topLeaderboard, bottomLeaderboard }: Props) {
  return (
    <div className="mx-auto flex max-w-xl flex-col justify-center p-6 text-center">
      <span>
        Raiderate is a reputation platform for ARC Raiders players. It allows
        users to check player profiles, view ratings, read feedback from other
        players, and leave reviews based on real in-game experience. The
        platform also helps report toxic, unfair, or suspicious behavior, giving
        the community more transparency and accountability.
      </span>
      <Link
        href="/rate"
        className="my-5 inline-flex justify-center gap-2 rounded border p-2"
      >
        <Star className="h-5 w-5" />
        <span>Rate player</span>
      </Link>
      <span className="text-[#2afe7f]">Highest Reputation</span>
      {topLeaderboard && <LeaderboardView leaderboard={topLeaderboard} />}
      <span className="text-[#fdd333]">Lowest Reputation</span>
      {bottomLeaderboard && <LeaderboardView leaderboard={bottomLeaderboard} />}
    </div>
  );
}
