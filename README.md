# ReaktorPreAssignment
My implementation of the Reaktor assessment. Try now: https://reaktorpreassignment20200130123350.azurewebsites.net/

On Debian and Ubuntu systems, there is a file called /var/lib/dpkg/status that holds information about software packages that the system knows about. This small .NET app service exposes some key information about packages in the file via an HTML interface.

1. The index page lists installed packages alphabetically with package names as links.
2. When following each link, you arrive at a piece of information about a single package. The following information is included:
  a. Name
  b. Description
  c. The names of the packages the current package depends on (skip version numbers)
  d. Reverse dependencies, i.e. the names of the packages that depend on the current package
3. The dependencies and reverse dependencies are clickable and the user can navigate the package structure by clicking from package to package.
4. Only looking at the Depends field. Ignoring other fields that work kind of similarly, such as Suggests and Recommends.
5. Sometimes there are alternates in a dependency list, separated by the pipe character |. When rendering such dependencies, rendering any alternative that maps to a package name that has an entry in the file as a link and printing the name of the package name for other packages.
6. The section “Syntax of control files” of the Debian Policy Manual applies to the input data.
7. The solution runs on non-Debian systems as well.


