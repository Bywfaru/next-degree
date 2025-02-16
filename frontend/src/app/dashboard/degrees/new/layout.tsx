import type { Metadata } from 'next';
import type { FC, PropsWithChildren } from 'react';

export const metadata: Metadata = {
  title: 'New degree | Next Degree',
};

const NewDegreeLayout: FC<Required<PropsWithChildren>> = ({ children }) => {
  return children;
};

export default NewDegreeLayout;
