import clsx from 'clsx';
import Link from 'next/link';
import type { FC } from 'react';

const Status = {
  NOT_STARTED: 0,
  IN_PROGRESS: 1,
  COMPLETED: 2,
  ABANDONED: 3,
};

const STATUS_TEXT: Record<typeof Status[keyof typeof Status], string> = {
  [Status.NOT_STARTED]: 'Not Started',
  [Status.IN_PROGRESS]: 'In Progress',
  [Status.COMPLETED]: 'Completed',
  [Status.ABANDONED]: 'Abandoned',
};

export type DegreeDashboardItemProps = {
  name: string;
  status: number;
  completedCredits: number;
  totalCredits: number;
  href?: string;
}

export const DegreeDashboardItem: FC<DegreeDashboardItemProps> = ({
  completedCredits,
  totalCredits,
  name,
  status,
  href = '#',
}) => {
  const statusText = STATUS_TEXT[status];
  const percentageCompleted = (completedCredits / totalCredits);
  const isNotStartedOrAbandoned = status === Status.NOT_STARTED || status === Status.ABANDONED;

  return <Link href={href}
    className={clsx([
      'flex',
      'flex-col',
      'gap-2.5',
      'w-full',
      'min-h-[170px]',
      'px-2.5',
      'py-5',
      'bg-white',
      'border',
      'border-primary',
      'rounded-lg',
      'h-full',
      'hover:bg-primary',
      'hover:text-white',
      'transition',
    ])}
  >
    <p className={clsx([
      'font-bold',
      'text-ellipsis',
      'line-clamp-2',
      'leading-tight',
    ])}
    >{name}</p>
    <p className={clsx([
      {
        'text-text-primary/50': status === Status.NOT_STARTED,
        'text-grade-fair': status === Status.IN_PROGRESS,
        'text-grade-good': status === Status.COMPLETED,
        'text-grade-poor': status === Status.ABANDONED,
      },
    ])}
    >{statusText}</p>
    <p className={clsx(['flex', 'flex-col', 'leading-none'])}>
      <span><span className={clsx([
        'font-bold',
        'text-2xl',
        isNotStartedOrAbandoned && 'text-text-primary/50',
        !isNotStartedOrAbandoned && {
          'text-grade-poor': percentageCompleted <= 0.5,
          'text-grade-fair': percentageCompleted > 0.5 && percentageCompleted < 1,
          'text-grade-good': percentageCompleted >= 1,
        },
      ])}
      >{completedCredits.toFixed(1)}</span>/{totalCredits.toFixed(1)}</span>
      <span>credits earned</span>
    </p>
  </Link>;
};