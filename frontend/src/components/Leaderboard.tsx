import {Leaderboard} from "@/shared/players/types";
import Link from "next/link";

type Props = { leaderboard: Leaderboard };

export default function LeaderboardView({leaderboard}: Props) {
    return (
        <div className="grid">
            {leaderboard.items.map((item) => (
                <Link
                    href={`/player/${item.nickname}`}
                    key={item.position}
                    className="flex items-center justify-between p-1"
                >
                    <div className="flex items-center gap-3">
                                <span className="text-right text-gray-500">
                                    {item.position + 1}
                                </span>
                        <span>{item.nickname}</span>
                    </div>

                    <span>{item.rating}</span>
                </Link>
            ))}
        </div>
    );
}