## What?
The idea of this project is to be able to generate a list of features and tickets that have gone into a particular build by looking at the differences between two tags or branches.
For example to see what when in version 2 of a specific project, we could compare the tags `1.0.0` and `2.0.0`

## Why?
This list of differences is useful for validating and confirming against an expected list of tickets and items as well as serving as a nice audit trail of what a particular build is adding/changing.

## How?
This project uses [GitHub's OctoKit API](https://github.com/octokit) to pull the data directly from GitHub without having to have the repositories on your machine.
It does this by utilising a GitHub access token that you provide.

## Limitations?
The repositories you can access are limited by the acess token you provide.  If you provide a limited rights token you may not be able to pull the differences for one or more of the specified repositories.

Uses .NET secrets for sensitive data such as access tokens.

The following command will set up the access token

```bash
dotnet user-secrets set "Secrets:GitHubAccessToken" "SECRET_VALUE"
```