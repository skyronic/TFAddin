#TaskForce for MonoDevelop
### Task oriented workflow.

## What is TaskForce?
I'm not too sure of it right now, but come back in a few weeks for a more concrete definition. Roughly speaking, it's a clone of Mylyn for MonoDevelop and a little more.

## Building TaskFoce
This is a guide to build TaskForce. Please contact me if it doesen't work.
1. Check out the MonoDevelop SVN tree and build.
2. Clone the git repository into extras and rename to MonoDevelop.TaskForce 
   	cd extras/
   	git clone git://github.com/ninjagod/TFAddin.git
   	mv TFAddin MonoDevelop.TaskForce
3. Add MonoDevelop.TaskForce.sln to the "Extras" workspace in the main MonoDevelop project.
4. Set the output path of the solution to "./main/build/addins/MonoDevelop.TaskForce" (this should be there by default).
5. Change the References on the project to use the local MonoDevelop .dlls rather than the one in the Global assembly cache.

## Author
This is being built as a Google summer of code project for MonoDevelop by Anirudh Sanjeev <andy@ninjagod.com> (http://github.com/ninjagod )under the mentorship of Michael Hutchinson <mjhutchinson@gmail.com>.

## Supported bug providers
* local
* bugzilla
=======

## Work Items:
* Finish bugzilla integration

