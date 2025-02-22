'use server';

import { getDegreeById } from '@/app/actions/degrees';
import { notFound } from 'next/navigation';
import type { FC } from 'react';

type PageProps = {
  params: Promise<{ id: string }>;
}

const DegreePage: FC<PageProps> = async ({ params }) => {
  const { id } = await params;
  const degree = await getDegreeById(id);

  if (!degree.success) return notFound();

  return (
    <div>
      <h1>{degree.data.data.name}</h1>
    </div>
  );
};

export default DegreePage;