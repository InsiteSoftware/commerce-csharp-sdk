# How to contribute

The B2B Commerce Cloud API SDK is open source software, and we welcome your contributions and suggestions for improvement to the package. The following instructions explain how you can work with the repository to share your improvements with the Optimizely developer community.

Before contributing, please read the [code of conduct](https://github.com/episerver/Foundation/blob/develop/docs/code-of-conduct.md).  Contributions are greatly appreciated via forks. Optimizely retains control of the direction of the project and reserves the right to close issues and PRs that don not align with the project roadmap.

## Contribution process

* Create a new issue in GitHub. _Note: If you are submitting a fix for a bug you found yourself, you will still need to report it._
* Assign this issue to yourself.
* Create branch off develop named XXX-shortname where XXX is the issue number in Github, for example "bugfix/1231-thumbnails-for-media". See "Branching" below for details.
* For bug fixes add a unit test to catch the bug before continuing.
* __Test__, code, __test__, code, __test__!
* When finished, write a commit message according to the commit message guidelines below.
* Before creating a pull request, pull the latest changes from develop into your branch and resolve any conflicts.
* Create a pull request with your changes and squash your changes to a single commit (unless multiple commits makes sense for some reason).
* Submit your pull request and await review.

Before submitting your pull request, take a few moments to make sure you've addressed some common issues reviewers will look for in your PR:
* Is the code documented?
* Are the tests up to date and do they test the new code appropriately?
* Are the correct issue ids present in the commit message and branch name?
* Did you follow the commit message guidelines? Your commit message becomes part of the main branch history, so make sure it clearly explains your contribution!

## Accepting pull requests
Pull request are the official mechanism for code review. Someone on the team signs off on the code contained in your pull request when approving it for merger. We strongly encourage you to proactively review code before you push to the repository but this is not enforced and developers choose how frequently do so.

All developers are encouraged to read, review, and comment on pull requests to make sure code reviews are a collaborative effort.

Once a pull request is approved, always delete the source branch when merging.

## Branching Model

We use the workflow described in https://www.atlassian.com/git/workflows#!workflow-gitflow with some minor modifications.

### Main branch
Should always contain tested, working, releasable code. You can only make changes to main by creating pull requests that must be reviewed and accepted.

### Develop branch

Acts as integration branch for feature and bugfix branches that should go into the next release. You can only get code onto develop by creating pull requests that then need to be reviewed and accepted. PRs should only contain completely implemented work items.

### Feature branches

Created from develop and should be named feature/`<issue id in github>-<short description>`. For example, to work on "User Story 35: Remove the 'Classic' link stage in the API and only use permanent links" you would create a branch from develop named "feature/35-remove-classic-links". Note that the `<short description>` is all lower-case with hyphens.

You can request merger of your branch to the develop branch by creating a pull request.

As a reminder, always pull develop into your feature branch and resolve any merge conflicts within your own branch prior to submitting a pull request for merger into develop.

### Bugfix branches
Created from develop and should be named bugfix/`<issue id in github>-<short description>`. For example to work on "Bug 111571: MVC rendering of built-in properties" you should create a branch named something like "bugfix/111571-mvc-rendering-properties". Note that the `<short description>` is all lower-case with hyphens.

Merge to the develop branch by creating a pull request.


## Commit Message Guidelines ##

To make the commit history easier to read and the changes easier to understand, create your commit messages according to the rules below, which are adapted from these [seven rules](http://chris.beams.io/posts/git-commit/#seven-rules).

* Separate the subject from body with a blank line.
* Limit the subject to 50 characters.
* Capitalize the subject line.
* Do not end the subject line with a period.
* Wrap the body at 72 characters.
* Use the body to explain what and why instead of how.
* Add a reference to the associated task/issue.

**Example commit message**  
Handle multiple errors from the server  

Extended the message view model with additional information displayed beneath
the error message in the alert dialog. Used the additional information in the
mark as a ready to publish method to handle errors that may occur when dealing
with multiple items.  
