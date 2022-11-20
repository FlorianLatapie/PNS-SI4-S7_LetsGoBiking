
package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour Feature complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="Feature"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="bbox" type="{http://schemas.microsoft.com/2003/10/Serialization/Arrays}ArrayOfdouble" minOccurs="0"/&gt;
 *         &lt;element name="geometry" type="{http://schemas.datacontract.org/2004/07/RoutingServer}Geometry" minOccurs="0"/&gt;
 *         &lt;element name="properties" type="{http://schemas.datacontract.org/2004/07/RoutingServer}Properties" minOccurs="0"/&gt;
 *         &lt;element name="type" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Feature", propOrder = {
    "bbox",
    "geometry",
    "properties",
    "type"
})
public class Feature {

    @XmlElementRef(name = "bbox", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<ArrayOfdouble> bbox;
    @XmlElementRef(name = "geometry", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<Geometry> geometry;
    @XmlElementRef(name = "properties", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<Properties> properties;
    @XmlElementRef(name = "type", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> type;

    /**
     * Obtient la valeur de la propriété bbox.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfdouble }{@code >}
     *     
     */
    public JAXBElement<ArrayOfdouble> getBbox() {
        return bbox;
    }

    /**
     * Définit la valeur de la propriété bbox.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfdouble }{@code >}
     *     
     */
    public void setBbox(JAXBElement<ArrayOfdouble> value) {
        this.bbox = value;
    }

    /**
     * Obtient la valeur de la propriété geometry.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link Geometry }{@code >}
     *     
     */
    public JAXBElement<Geometry> getGeometry() {
        return geometry;
    }

    /**
     * Définit la valeur de la propriété geometry.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link Geometry }{@code >}
     *     
     */
    public void setGeometry(JAXBElement<Geometry> value) {
        this.geometry = value;
    }

    /**
     * Obtient la valeur de la propriété properties.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link Properties }{@code >}
     *     
     */
    public JAXBElement<Properties> getProperties() {
        return properties;
    }

    /**
     * Définit la valeur de la propriété properties.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link Properties }{@code >}
     *     
     */
    public void setProperties(JAXBElement<Properties> value) {
        this.properties = value;
    }

    /**
     * Obtient la valeur de la propriété type.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getType() {
        return type;
    }

    /**
     * Définit la valeur de la propriété type.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setType(JAXBElement<String> value) {
        this.type = value;
    }

}
