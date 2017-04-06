/**
 * Copyright by Nocksoft
 * https://www.nocksoft.de
 * https://www.nocksoft.de/index.php?tutorial=visual-c-sharp-arbeiten-mit-ini-dateien
 * -----------------------------------
 * Erstellt von:	Rafael Nockmann
 * Letzte Änderung:	19.02.2016
 * Version:         1.0.2
 *
 * Sprache: Visual C#
 *
 * Beschreibung:
 * Stellt grundlegende Funktionen bereit, um mit INI-Dateien zu arbeiten.
 *
 * ##############################################################################################
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Nocksoft.IO.ConfigFiles
{
    public class INIFile
    {
        private string _File;

        /// <summary>
        /// Aufruf des Konstruktors initialisiert ein Objekt der Klasse INIFile.
        /// </summary>
        /// <param name="file">INI-Datei, auf der zugegriffen werden soll.</param>
        /// <param name="createFile">Gibt an, ob die Datei erstellt werden soll, wenn diese nicht vorhanden ist.</param>
        public INIFile(string file, bool createFile = false)
        {
            if(createFile == true && File.Exists(file) == false)
            {
                FileInfo fileInfo = new FileInfo(file);
                FileStream fileStream = fileInfo.Create();
                fileStream.Close();
            }
            _File = file;
        }

        #region Öffentliche Methoden

        /// <summary>
        /// Entfernt alle Kommentare und leeren Zeilen aus einer kompletten Section und gibt diese zurück.
        /// Die Methode ist nicht Case-sensitivity und ignoriert daher Groß- und Kleinschreibung.
        /// Der Rückgabewert enthält keine Leerzeichen.
        /// </summary>
        /// <param name="section">Name der angeforderten Section.</param>
        /// <returns>Gibt die komplette Section zurück.</returns>
        public List<string> GetSection(string section)
        {
            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section).ToLower();

            List<string> completeSection = new List<string>();
            bool sectionStart = false;

            // Liest die Zieldatei ein
            string[] fileArray = File.ReadAllLines(this._File);

            foreach (var item in fileArray)
            {
                if (item.Length > 0)
                {
                    // Wenn die gewünschte Section erreicht ist
                    if (item.Replace(" ", "").ToLower() == section)
                    {
                        sectionStart = true;
                    }
                    // Wenn auf eine neue Section getroffen wird, wird die Schleife beendet
                    if (sectionStart == true && item.Replace(" ", "").ToLower() != section && item.Replace(" ", "").Substring(0, 1) == "[" && item.Replace(" ", "").Substring(item.Length - 1, 1) == "]")
                    {
                        break;
                    }
                    if (sectionStart == true)
                    {
                        // Wenn der Eintrag kein Kommentar und kein leerer Eintrag ist, wird er der List<string> completeSection hinzugefügt
                        if (item.Replace(" ", "").Substring(0, 1) != ";" && !String.IsNullOrWhiteSpace(item))
                        {
                            completeSection.Add(ReplaceScpacesAtStartAndEnd(item));
                        }
                    }
                }
            }
            return completeSection;
        }

        /// <summary>
        /// Die Methode gibt einen Wert zum dazugehörigen Key zurück.
        /// Die Methode ist nicht Case-sensitivity und ignoriert daher Groß- und Kleinschreibung.
        /// </summary>
        /// <param name="section">Name der angeforderten Section.</param>
        /// <param name="key">Name des angeforderten Keys.</param>
        /// <param name="convertKeyToLower">Wenn "true" übergeben wird, wird der Rückgabewert in Kleinbuchstaben zurückgegeben.</param>
        /// <returns>Gibt, wenn vorhanden, den Wert zu dem angegebenen Key in der angegeben Section zurück.</returns>
        public string GetValue(string section, string key, bool convertValueToLower = false)
        {
            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section).ToLower();
            key = key.ToLower();

            List<string> completeSection = GetSection(section);

            foreach (var item in completeSection)
            {
                // Auf Key prüfen
                if (item.Contains("=") && !item.Contains("[") && !item.Contains("]"))
                {
                    string[] keyAndValue = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (keyAndValue[0].ToLower() == key && keyAndValue.Count() > 1)
                    {
                        if (convertValueToLower == true)
                        {
                            keyAndValue[1] = keyAndValue[1].ToLower();
                        }
                        return keyAndValue[1];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Ändert einen Wert des dazugehörigen Schlüssels in der angegebenen Section.
        /// </summary>
        /// <param name="section">Name der Section, in dem sich der Schlüssel befindet.</param>
        /// <param name="key">Name des Schlüssels, dessen Wert geändert werden soll.</param>
        /// <param name="value">Neuer Wert.</param>
        /// <param name="convertValueToLower">Wenn "true" übergeben wird, wird der Wert in Kleinbuchstaben gespeichert.</param>
        public void SetValue(string section, string key, string value, bool convertValueToLower = false)
        {
            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section).ToLower();
            string keyToLower = key.ToLower();

            // Prüft, ob die gesuchte Section gefunden wurde
            bool sectionFound = false;

            List<string> newSettings = new List<string>();

            // Liest die Zieldatei ein
            string[] fileArray = File.ReadAllLines(this._File);

            // Wenn die Zieldatei nicht leer ist...
            if (fileArray.Length > 0)
            {
                // ...wird jede Zeile durchsucht
                for (int i = 0; i < fileArray.Length; i++)
                {
                    if (fileArray[i].Length > 0)
                    {
                        // Wenn die gewünschte Section erreicht ist
                        if (fileArray[i].Replace(" ", "").ToLower() == section)
                        {
                            sectionFound = true;

                            // Enthält die komplette Section, in der sich der Zielschlüssel befindet
                            List<string> targetSection = GetSection(section);

                            // Jeden Eintrag in der Section, in der sich der Zielschlüssel befindet, durchgehen
                            for (int x = 0; x < targetSection.Count; x++)
                            {
                                // Wenn der Zielschlüssel gefunden ist
                                string[] targetKey = targetSection[x].Split(new string[] { "=" }, StringSplitOptions.None);
                                if (targetKey[0].ToLower() == keyToLower)
                                {
                                    // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
                                    if (convertValueToLower == true)
                                    {
                                        newSettings.Add(keyToLower + "=" + value.ToLower());
                                    }
                                    else
                                    {
                                        newSettings.Add(key + "=" + value);
                                    }
                                    i = i + x;
                                    break;
                                }
                                else
                                {
                                    newSettings.Add(targetSection[x]);
                                    // Wenn Key nicht vorhanden ist, wird dieser erzeugt
                                    if (x == targetSection.Count - 1 && targetKey[0].ToLower() != keyToLower)
                                    {
                                        // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
                                        if (convertValueToLower == true)
                                        {
                                            newSettings.Add(keyToLower + "=" + value.ToLower());
                                        }
                                        else
                                        {
                                            newSettings.Add(key + "=" + value);
                                        }
                                        i = i + x;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            newSettings.Add(fileArray[i]);
                            // Wenn Section nicht vorhanden ist, wird diese erzeugt
                            if (i == fileArray.Length - 1 && fileArray[i].Replace(" ", "").ToLower() != section && sectionFound == false)
                            {
                                newSettings.Add(null);
                                newSettings = CreateSection(newSettings, section, value, key, convertValueToLower);
                                break;
                            }
                        }
                    }
                    else
                    {
                        newSettings.Add(fileArray[i]);

                        // Wenn Section nicht vorhanden ist, wird diese erzeugt
                        if (sectionFound == false && i + 1 == fileArray.Length)
                        {
                            newSettings = CreateSection(newSettings, section, value, key, convertValueToLower);
                            break;
                        }
                    }
                }
            }
            else
            {
                newSettings = CreateSection(newSettings, section, value, key, convertValueToLower);
            }

            StreamWriter writer = new StreamWriter(this._File);
            foreach (var item in newSettings)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }

        #endregion

        #region Private Methoden

        /// <summary>
        /// Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
        /// </summary>
        /// <param name="section">Section, die auf korrektes Format geprüft werden soll.</param>
        /// <returns>Gibt Section in dieser Form zurück: [section]</returns>
        private string CheckSection(string section)
        {
            if (section.Substring(0, 1) != "[" && section.Substring(section.Length - 1, 1) != "]")
            {
                section = "[" + section + "]";
            }
            return section;
        }

        /// <summary>
        /// Entfernt voranstehende und hintenstehende Leerzeichen bei Sections, Keys und Values.
        /// </summary>
        /// <param name="item">String, der gekürzt werden soll.</param>
        /// <returns>Gibt einen gekürzten String zurück.</returns>
        private string ReplaceScpacesAtStartAndEnd(string item)
        {
            // Wenn der Eintrag einen Schlüssel und einen Wert hat
            if (item.Contains("=") && !item.Contains("[") && !item.Contains("]"))
            {
                string[] keyAndValue = item.Split(new string[] { "=" }, StringSplitOptions.None);
                return ReplaceScpacesAtStartAndEnd(keyAndValue[0]) + "=" + ReplaceScpacesAtStartAndEnd(keyAndValue[1]);
            }

            string itemWithoutSpace = item;

            // Entfernt die voranstehenden Leerzeichen
            for (int i = 0; i < item.Length; i++)
            {
                if (item.Substring(i, 1) != " ")
                {
                    itemWithoutSpace = item.Substring(i, item.Length - i);
                    break;
                }
            }

            // Entfernt die hintenstehenden Leerzeichen
            for (int i = 0; i < itemWithoutSpace.Length; i++)
            {
                if (itemWithoutSpace.Substring(itemWithoutSpace.Length - i - 1, 1) != " ")
                {
                    itemWithoutSpace = itemWithoutSpace.Substring(0, itemWithoutSpace.Length - i);
                    break;
                }
            }
            return itemWithoutSpace;
        }

        /// <summary>
        /// Legt eine neue Section an.
        /// </summary>
        /// <param name="newSettings">Liste newSettings aus SetValue.</param>
        /// <param name="section">section die angelegt werden soll.</param>
        /// <param name="value">Wert der hinzugefügt werden soll.</param>
        /// <param name="key">Schlüssel der hinzugefügt werden soll.</param>
        /// <param name="convertValueToLower">Gibt an, ob Schlüssel und Wert in Kleinbuchstaben abgespeichert werden sollen.</param>
        /// <returns></returns>
        private List<string> CreateSection(List<string> newSettings, string section, string value, string key, bool convertValueToLower)
        {
            string keyToLower = key.ToLower();

            newSettings.Add(section);
            // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
            if (convertValueToLower == true)
            {
                newSettings.Add(keyToLower + "=" + value.ToLower());
            }
            else
            {
                newSettings.Add(key + "=" + value);
            }
            return newSettings;
        }

        #endregion
    }
}
