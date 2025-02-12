import clsx from 'clsx';
import Link from 'next/link';
import type { FC } from 'react';

export type DegreeDashboardItemProps = {
  name: string;
  status: 'not-started' | 'in-progress' | 'completed' | 'abandoned';
  completedCredits: number;
  totalCredits: number;
  href?: string;
}

const STATUS_TEXT: Record<DegreeDashboardItemProps['status'], string> = {
  'not-started': 'Not Started',
  'in-progress': 'In Progress',
  'completed': 'Completed',
  'abandoned': 'Abandoned',
};

export const DegreeDashboardItem: FC<DegreeDashboardItemProps> = ({
  completedCredits,
  totalCredits,
  name,
  status,
  href = '#',
}) => {
  const statusText = STATUS_TEXT[status];
  const percentageCompleted = (completedCredits / totalCredits);
  const isNotStartedOrAbandoned = status === 'abandoned' || status === 'not-started';

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
        'text-text-primary/50': status === 'not-started',
        'text-grade-fair': status === 'in-progress',
        'text-grade-good': status === 'completed',
        'text-grade-poor': status === 'abandoned',
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