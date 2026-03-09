type Props = {
    value: number;
    className?: string;
}

export default function SignedNumber({value, className}: Props) {
    const color =
        value > 0 ? "text-[#2afe7f]" :
            value < 0 ? "text-[#fdd333]" :
                "text-gray-200";

    const text = value > 0 ? `+${value}` : `${value}`;

    return <span className={[color, className ?? ""].join(" ")}>{text}</span>;
}