'use client';

import { loginUser } from '@/app/actions/users';
import { Button, Input } from '@/components';
import { zodResolver } from '@hookform/resolvers/zod';
import clsx from 'clsx';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { type FC, useState } from 'react';
import { type SubmitHandler, useForm } from 'react-hook-form';
import { z } from 'zod';

const LOGIN_SCHEMA = z.object({
  email: z.string().email(),
  password: z.string(),
});

type LoginSchema = z.infer<typeof LOGIN_SCHEMA>;

export const LoginForm: FC = () => {
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm<LoginSchema>({
    resolver: zodResolver(LOGIN_SCHEMA),
  });
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);

  const processForm: SubmitHandler<LoginSchema> = async ({
    email,
    password,
  }) => {
    setIsLoading(true);

    const response = await loginUser({ email, password });

    if (response.success) return router.push('/degrees');

    setFormError(response.error);
    setIsLoading(false);
  };

  return (
    <form
      onSubmit={handleSubmit(processForm)}
      className={clsx(['flex', 'flex-col', 'w-full', 'gap-2.5'])}
    >
      <Input
        type="email"
        register={register('email')}
        label="Email"
        placeholder="Enter your email address"
        errors={errors}
        disabled={isLoading}
        fullWidth
      />

      <Input
        type="password"
        register={register('password')}
        label="Password"
        placeholder="Enter your password"
        errors={errors}
        disabled={isLoading}
        fullWidth
      />

      {!!formError && (
        <p className={clsx('text-red-500', 'text-center')}>{formError}</p>
      )}

      <Button type="submit" loading={isLoading}>
        Login
      </Button>

      <p className="text-center">
        Don&apos;t have an account?{' '}
        <Link
          href="/register"
          className={clsx(
            'underline',
            'text-primary',
            'hover:text-primary/50',
            'transition',
          )}
        >
          Sign up here
        </Link>
      </p>
    </form>
  );
};
